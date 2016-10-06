using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpMap;
using SharpMap.Layers;
using SharpMap.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using GisSharpBlog.NetTopologySuite;

namespace GSharpViewer
{

	public partial class frmMain : Form
	{
		// Map
		private SharpMap.Map map;
		private bool isMapOpen = false;
		private SharpMap.Layers.VectorLayer vLayer;
		public int width;
		public int height;
		
		//TreeView
		private TreeNode rootNode;

		public frmMain()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//Größe der Karte = Größe des TabPages tabMap
			width = tabMap.Width;
			height = tabMap.Height;
		}
		
		private SharpMap.Layers.VectorLayer loadShapefile(OpenFileDialog _openFileDialog)
		{
			//--> Definieren des Layernamens und des Pfads
			string dataPath = String.Empty;
			string dataName = String.Empty;
			
			dataPath = _openFileDialog.FileName;
			int indexOfPoint = _openFileDialog.FileName.IndexOf('.');
			dataName = _openFileDialog.FileName.Remove(indexOfPoint, 4);
			int indexOfShpName = dataName.LastIndexOf(@"\");
			dataName = dataName.Substring(indexOfShpName);
			dataName = dataName.Substring(1);
			
			vLayer = new SharpMap.Layers.VectorLayer(dataName);

			//Mit dem File-basierten Spatial Index (Quadtree) versuchen
			try
			{
				vLayer.DataSource = new SharpMap.Data.Providers.ShapeFile(dataPath, true);
				vLayer.DataSource.Open();
			}
			catch (Exception ex) { MessageBox.Show("Fehler beim Laden des Layers " + vLayer.LayerName + " aufgetreten." + Environment.NewLine + 
												   "Die Meldung lautet: " + ex.Message + Environment.NewLine +
												   "Stack Trace: " + Environment.NewLine + ex.StackTrace.ToString(),
													"Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
			return vLayer;
		}


		private SharpMap.Map createMap(int width, int height)
		{
			//Initialisieren der Karte
			map = new Map(new Size(width, height));
			map.BackColor = Color.AliceBlue;

			return map;
		}

		private void addLayer(SharpMap.Layers.Layer _layer)
		{
			mapImage1.Map.Layers.Add(_layer);

			//Layernamen zum TreeView hinzufügen
			Boolean isRemoved = false;
			populateTreeView(_layer.LayerName, isRemoved);

			//Zoom to Layer
			_layer.Enabled = true;
			mapImage1.Map.ZoomToBox(_layer.Envelope.Grow(0.5));
			mapImage1.Refresh();
		}
		
		private void populateTreeView(string layername, Boolean _isRemoved)
		{
			rootNode = treeView1.Nodes[0];
			int insertIdx = 0;
			
			if (_isRemoved)
			{
				rootNode.Nodes.RemoveByKey(layername);
				treeView1.ExpandAll();
			}
			else
			{
				// "Insert" statt "Add" nutzen, weil ein neuer Layer immer als erstes
				// Element nach dem Root-Knoten eingefügt werden soll
				rootNode.Nodes.Insert(insertIdx, layername, layername);
				treeView1.ExpandAll();
			}
		}

		private void paintLayer(SharpMap.Layers.VectorLayer vLayer)
		{
			//Zufällige Farbe
			Random random = new Random();
			int rot = random.Next(0, 255);
			int gruen = random.Next(0, 255);
			int blau = random.Next(0, 255);

			string geometrytype = getGeometryType(vLayer);

			   switch (geometrytype)
			{
				case "POINT":
					//bm.SetPixel(2, 2, Color.FromArgb(rot, gruen, blau));
					//vLayer.Style.Symbol = new Bitmap(bm);
					//vLayer.Style.SymbolScale = 5;
					break;
				case "LINE":
					vLayer.Style.Line = new Pen(Color.FromArgb(rot, gruen, blau));
					break;
				case "LINESTRING":
					vLayer.Style.Line = new Pen(Color.FromArgb(rot, gruen, blau));
					break;
				case "MULTILINESTRING":
					vLayer.Style.Line = new Pen(Color.FromArgb(rot, gruen, blau));
					break;
				case "POLYGON":
					vLayer.Style.Fill = new SolidBrush(Color.FromArgb(rot, gruen, blau));
					vLayer.Style.EnableOutline = true;
					vLayer.Style.Outline = new Pen(Color.Black);
					break;
				case "MULTIPOLYGON":
					vLayer.Style.Fill = new SolidBrush(Color.FromArgb(rot, gruen, blau));
					vLayer.Style.EnableOutline = true;
					vLayer.Style.Outline = new Pen(Color.Black);
					break;
			}
		}

		private string getGeometryType(SharpMap.Layers.VectorLayer vLayer)
		{
			try
			{
				string s = vLayer.DataSource.GetFeature(0).Geometry.AsText().ToString();
				int i = s.IndexOf(' ');
				string geometryType = s.Remove(i, s.Length - i);
				return geometryType;
			}
			catch  { return String.Empty; }
		}
		
		private string getSelectedLayer(TreeView treeView)
		{
			string layername = String.Empty;
			
			rootNode = treeView.Nodes[0];
			TreeNodeCollection subNodes = rootNode.Nodes;

			foreach (TreeNode node in subNodes)
			{
				if (node.IsSelected)
				{
					layername = node.Text;
				}
			}
			return layername;
		}
		
		private int getLayerIndex(string layername, SharpMap.Map _map)
		{
			int layerIndex = 0;
			ILayer testLayer = mapImage1.Map.GetLayerByName(layername);
			layerIndex = _map.Layers.IndexOf(testLayer);
			return layerIndex;
		}
		
		private void toolStripbtnLoad_Click(object sender, EventArgs e)
		{
			// Lade Shapefiles
			DialogResult result = openFileDialog1.ShowDialog();
			// Abfangen des Abbruchs der Benutzereingabe beim Lade-Dialog
			if (result != DialogResult.Cancel)
			{
				mapImage1.Cursor = Cursors.WaitCursor;
				// Erzeuge Karte
				if (isMapOpen == false)
				{
					map = createMap(width, height);
					isMapOpen = true;
				}
				try
				{
					VectorLayer vLayer = loadShapefile(openFileDialog1);
					// Wenn das Oeffnen der Datenquelle funktioniert hat..
					if (vLayer.DataSource != null)
					{
						if (vLayer.DataSource.IsOpen)
						{
							paintLayer(vLayer);
							addLayer(vLayer);
						}
					}
				}
				catch (SharpMap.Layers.DuplicateLayerException ex)
				{
					MessageBox.Show("Der Layer " + ex.DuplicateLayerName + " ist bereits geladen.",
										"Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				mapImage1.Cursor = Cursors.Default;
			}
		}
		
		void BtnPanClick(object sender, EventArgs e)
		{
			mapImage1.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
		}
		
		void BtnZoomInClick(object sender, EventArgs e)
		{
			mapImage1.ActiveTool = SharpMap.Forms.MapImage.Tools.ZoomIn;
		}
		
		void BtnZoomOutClick(object sender, EventArgs e)
		{
			mapImage1.ActiveTool = SharpMap.Forms.MapImage.Tools.ZoomOut;
		}
		
		void BtnFullExtentClick(object sender, EventArgs e)
		{
			mapImage1.Map.ZoomToExtents();
			mapImage1.Refresh();
		}
		
		void BtnInfoClick(object sender, EventArgs e)
		{
			//Überprüfe, welcher Layer im TreeView selektiert wurde
			string layername;
			int layerIndex;
			
			layername = getSelectedLayer(treeView1);
			
			if (layername != String.Empty)
			{
				toolStripStatusLabel2.Text = "Layer "+  layername + " wird abgefragt.";
				layerIndex = getLayerIndex(layername, mapImage1.Map);
				if (layerIndex >= 0)
				{
					mapImage1.QueryLayerIndex = layerIndex;
					mapImage1.ActiveTool = SharpMap.Forms.MapImage.Tools.Query;
					mapImage1.Cursor = Cursors.Help;
				}
			}
			else
			{
				MessageBox.Show("Fehler bei der Abfrage. Bitte zuerst einen Layer auswählen.", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

		}
		
		void MapImage1MapQueriedDataSet(SharpMap.Data.FeatureDataSet data)
		{
			//Binde die Ergebnisse der Kartenabfrage (Info-Button) an das DataGridView
			dgvAttributes.DataSource = data.Tables[0];
			//Zeige Attribut-Infos
			tabControl1.SelectedIndex = 1;
		}
		
		void MapImage1SizeChanged(object sender, EventArgs e)
		{
			mapImage1.Refresh();
		}
	}
}