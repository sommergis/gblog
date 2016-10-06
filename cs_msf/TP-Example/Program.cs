/*
 * C#.Net example for solving an instance of a Transportation Problem with 
 * Microsoft Solver Foundation (MSF) 3.1
 * 
 * Johannes Sommer 2012
 * ----
 * G#.Blog
 * www.sommer-forst.de/blog
 */

using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using Microsoft.SolverFoundation.Services;

namespace GSharpBlog.Optimization.TP_Example
{
	class Program
	{
        /// <summary>
        /// Gets results of a SQL query through a OLE-DB connection.
        /// </summary>
        /// <param name="connectionStr">OLE-DB connection string</param>
        /// <param name="query">SQL query</param>
        /// <returns>DataSet</returns>
        private static DataSet getDataFromDB(string connectionStr, string query)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbConnection conn = new OleDbConnection(connectionStr);
            try {
                conn.Open();
                adapter.SelectCommand = new OleDbCommand(query, conn);
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception ex){ throw ex; }
            finally{ conn.Close(); }
        }

        public static void Main(string[] args)
		{
            // Access (2000-2003) DB ConnectionString
            //string connectionStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=tp.mdb";

            // Access (2007) DB ConnectionString
            string connectionStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=tp.accdb";

			// "Directive" means which Solver shall be used
			// e.g.: simplexDirective, interiorPointMethod
			string directive = "simplexDirective";
			string simplexAlgorithm = "Dual";
			string simplexPricingStrategy = "Partial";

			Console.WriteLine("Solving Transportation Problem..");
			Console.WriteLine("Settings: ");
			Console.WriteLine(" Solver Directive: "+directive);
			if (directive == "simplexDirective")
			{
				Console.WriteLine(" Simplex Algorithm: "+simplexAlgorithm);
				Console.WriteLine(" Simplex Pricing Strategy: "+simplexPricingStrategy);
			}
						
			// Transportation Problem als OML-Modell
			// Source: 
			// "Modeling with Excel+OML, a practical guide"
			// Autor: 
			// Amsterdam Optimization Modeling Group LLC
			// Erwin
			// 10/16/2009
			//
			string strModel = @"Model[
				Parameters[Sets,Source,Sink],
				Parameters[Reals,Supply[Source],Demand[Sink],Cost[Source,Sink]],
				Decisions[Reals[0,Infinity],flow[Source,Sink],TotalCost],
				Constraints[
					TotalCost == Sum[{i,Source},{j,Sink},Cost[i,j]*flow[i,j]],
					Foreach[{i,Source}, Sum[{j,Sink},flow[i,j]]<=Supply[i]],
					Foreach[{j,Sink}, Sum[{i,Source},flow[i,j]]>=Demand[j]]],
				Goals[Minimize[TotalCost]] ]";
			
			// Load OML-Model
			SolverContext context = SolverContext.GetContext();
			context.LoadModel(FileFormat.OML, new StringReader(strModel));
			context.CurrentModel.Name = "Transportation Problem";
			
			// Create Tables
			// Supply table
			DataTable pSupply = new DataTable();
			pSupply.Columns.Add("SupplyNode",Type.GetType("System.String"));
			pSupply.Columns.Add("Supply",Type.GetType("System.Int32"));
			
			// Demand table
			DataTable pDemand = new DataTable();
			pDemand.Columns.Add("DemandNode",Type.GetType("System.String"));
			pDemand.Columns.Add("Demand",Type.GetType("System.Int32"));
			
			// OD-Matrix
			DataTable pCost = new DataTable();
			pCost.Columns.Add("SupplyNode",Type.GetType("System.String"));
			pCost.Columns.Add("DemandNode",Type.GetType("System.String"));
			pCost.Columns.Add("Cost",Type.GetType("System.Double"));
							
            //// Fill tables
            // 1. Fill Supply
            string query = String.Empty;
            DataSet accessDS = new DataSet();
            query = "SELECT SupplyNode, Supply FROM Supply ORDER BY SupplyNode";
            accessDS = getDataFromDB(connectionStr, query);

            foreach (DataRow row in accessDS.Tables[0].Rows) {
                pSupply.Rows.Add(row[0].ToString(), row[1]);
            }

            // Clear
            query = String.Empty;
            accessDS.Clear();

            // 2.Fill Demand
            query = "SELECT DemandNode, Demand FROM Demand ORDER BY DemandNode";
            accessDS = getDataFromDB(connectionStr, query);

            foreach (DataRow row in accessDS.Tables[0].Rows) {
                pDemand.Rows.Add(row[0].ToString(), row[1]);
            }

            // Clear
            query = String.Empty;
            accessDS.Clear();

            // 3. Fill Arcs (or OD-Matrix)
            query = "SELECT SupplyNode, DemandNode, Cost FROM Arcs ORDER BY SupplyNode";
            accessDS = getDataFromDB(connectionStr, query);

            foreach (DataRow row in accessDS.Tables[0].Rows) {
                pCost.Rows.Add(row[0].ToString(), row[1].ToString(), row[2]);
            }

            //pSupply.Rows.Add("A", 10);
            //pSupply.Rows.Add("B", 18);
            //pSupply.Rows.Add("C", 12);
            //pSupply.Rows.Add("D", 8);
            //pSupply.Rows.Add("E", 12);
			
            //pDemand.Rows.Add("1", 25);
            //pDemand.Rows.Add("2", 35);
			
            //pCost.Rows.Add("A", "1", 15);
            //pCost.Rows.Add("A", "2", 11);
            //pCost.Rows.Add("B", "1", 13);
            //pCost.Rows.Add("B", "2", 16);
            //pCost.Rows.Add("C", "1", 11);
            //pCost.Rows.Add("C", "2", 9);
            //pCost.Rows.Add("D", "1", 13);
            //pCost.Rows.Add("D", "2", 6);
            //pCost.Rows.Add("E", "1", 18);
            //pCost.Rows.Add("E", "2", 4);
						
			// Bind values from tables to parameter of the OML model
			foreach (Parameter p in context.CurrentModel.Parameters)
			{
				switch (p.Name)
					{
						case "Supply":
							p.SetBinding(pSupply.AsEnumerable(), "Supply", "SupplyNode");
							break;
						case "Demand":
							p.SetBinding(pDemand.AsEnumerable(), "Demand", "DemandNode");
							break;
						case "Cost":
                            p.SetBinding(pCost.AsEnumerable(), "Cost", "SupplyNode", "DemandNode");
							break;
					}
			}
			
			// Sum initial supply and demand
			double origSupply;
			double origDemand;
			origSupply = Convert.ToDouble(pSupply.Compute("Sum(Supply)", null));
			origDemand = Convert.ToDouble(pDemand.Compute("Sum(Demand)", null));
			
			Solution solution = null;
			// SimplexDirective
			if (directive == "simplexDirective")
			{
				SimplexDirective dir = new SimplexDirective();
				switch (simplexAlgorithm) {
					case "Primal":
						dir.Algorithm = SimplexAlgorithm.Primal;
						break;
					case "Dual":
						dir.Algorithm = SimplexAlgorithm.Dual;
						break;
					default:
						dir.Algorithm = SimplexAlgorithm.Default;
						break;
				}
				switch (simplexPricingStrategy) {
					case "SteepestEdge":
						dir.Pricing = SimplexPricing.SteepestEdge;
						break;
					case "ReducedCost":
						dir.Pricing = SimplexPricing.ReducedCost;
						break;
					case "Partial":
						dir.Pricing = SimplexPricing.Partial;
						break;
					default:
						dir.Pricing = SimplexPricing.Default;
						break;
				}
				// Call solver
				solution = context.Solve(dir);
			}

			// Alternative: if "" MSF chooses solver
			if (directive == "")
			{
				// Solver-Aufruf
				solution = context.Solve();
			}

			// Fetch results: minimized total costs
			Report report = solution.GetReport();	
			double cost = 0;
			foreach (Decision desc in solution.Decisions) {
				if (desc.Name == "TotalCost")
				{
						foreach (object[] value in desc.GetValues()) {
							cost = Convert.ToDouble(value[0]);
						}
				}
			}
			
			Console.WriteLine("****************************************************************");
			if (directive == "simplexDirective")
				Console.WriteLine("Modelname: {0} \nQualtity: {1}\nSolveTime: {2}ms \nSolver: {3}\nSolver type: {4}\nTotal result: {5}",
			                  report.ModelName, report.SolutionQuality, report.SolveTime.ToString(), 
			                  (SimplexDirective)report.SolutionDirective as SimplexDirective, report.SolverType, cost);
			if (directive == "")
				Console.WriteLine("Modelname: {0} \nQualtity: {1}\nSolveTime: {2}ms \nSolver: {3}\nSolver type: {4}",
			                  report.ModelName, report.SolutionQuality, report.SolveTime.ToString(), 
			                  report.SolutionDirective, report.SolverType);
			Console.WriteLine("****************************************************************");
			if ( report.SolutionQuality == SolverQuality.Infeasible)
			{
				Console.WriteLine("Solution not possible\nSolver Status: {0}", report.SolutionQuality.ToString());
				return;
			}
			
			// Print out optimized results
			string result = String.Empty;
			double totalFlow = 0.0;
			foreach (Decision desc in solution.Decisions) {
				// flow as variable
				if (desc.Name == "flow")
				{
					foreach (object[] value in desc.GetValues()) {
						string source = value[1].ToString();
						string sink =  value[2].ToString();
						double flow = Convert.ToDouble(value[0]);
						
						// lookup km from arcs table
						DataRow[] rows = new DataRow[1];
						rows = pCost.Select("SupplyNode ='"+source+"' AND DemandNode ='"+sink+"'");
						double km = Convert.ToDouble(rows[0]["Cost"]);
						string sourceSink = String.Format("{0}_{1}", source, sink);
						if(flow != 0)
						{
							totalFlow += flow;
							result = result + "\n" + String.Format("\"{0}\";\"{1}\";\"{2}\";{3};{4}",
							                                       sourceSink, source, sink, flow, km);
						}
					}
                    Console.WriteLine(result);
				}
			}

			Console.WriteLine("\nResults:");
			Console.WriteLine("****************************************************************");
				
			Console.WriteLine("Supply:\t\t\t\t"+ origSupply.ToString() +" Shipping Units");
            Console.WriteLine("Demand:\t\t\t\t" + origDemand.ToString() + " Shipping Units");
            Console.WriteLine("Total Flow:\t\t\t" + totalFlow.ToString() + " Shipping Units");
			Console.WriteLine("****************************************************************");
			Console.WriteLine("Total optimized shipping km:\t"+ 
			                  cost.ToString() +"km" );
			Console.WriteLine("****************************************************************");	
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}