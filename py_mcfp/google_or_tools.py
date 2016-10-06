# import graph from google or-tools
from graph import pywrapgraph
 
# Create graph
# Attention: add +1 for number of nodes and arcs, if your node-IDs begin with 1
# because google or-tools mcf solver internal counters are strictly zero-based!
num_nodes = 4 + 1
num_arcs = 5 + 1
# args: NumNodes * NumArcs
G = pywrapgraph.StarGraph(num_nodes, num_arcs)
 
# create min cost flow solver 
min_cost_flow = pywrapgraph.MinCostFlow(G)
 
# add node to graph with positive supply for each supply node 
min_cost_flow.SetNodeSupply(1, 4)
# add node to graph with negative demand for each demand node 
min_cost_flow.SetNodeSupply(4, -4)
# you can ignore transshipment nodes with zero supply when you are working with
# the mcfp-solver of google or-tools
 
# add arcs to the graph
arc = G.AddArc(1, 2)
min_cost_flow.SetArcCapacity(arc, 4)
min_cost_flow.SetArcUnitCost(arc, 2)
arc = G.AddArc(1, 3)
min_cost_flow.SetArcCapacity(arc, 2)
min_cost_flow.SetArcUnitCost(arc, 2)
arc = G.AddArc(2, 3)
min_cost_flow.SetArcCapacity(arc, 2)
min_cost_flow.SetArcUnitCost(arc, 1)
arc = G.AddArc(2, 4)
min_cost_flow.SetArcCapacity(arc, 3)
min_cost_flow.SetArcUnitCost(arc, 3)
arc = G.AddArc(3, 4)
min_cost_flow.SetArcCapacity(arc, 5)
min_cost_flow.SetArcUnitCost(arc, 1)
 
# solve the min cost flow problem
# flowDict contains the optimized flow
# flowCost contains the total minimized optimum
min_cost_flow.Solve()
flowCost = min_cost_flow.GetOptimalCost()
 
print "Optimum: %s" %flowCost
