# import networkx
import networkx as nx
 
# create directed graph
G = nx.DiGraph()
 
# add node to graph with negative (!) supply for each supply node 
G.add_node(1, demand = -4)
 
# you can ignore transshipment nodes with zero supply when you are working with the mcfp-solver 
# of networkx
# add node to graph with positive (!) demand for each demand node
G.add_node(4, demand = 4)
 
# add arcs to the graph: fromNode,toNode,capacity,cost (=weight)
G.add_edge(1, 2, capacity = 4, weight = 2)
G.add_edge(1, 3, capacity = 2, weight = 2)
G.add_edge(2, 3, capacity = 2, weight = 1)
G.add_edge(2, 4, capacity = 3, weight = 3)
G.add_edge(3, 4, capacity = 5, weight = 1)
 
# solve the min cost flow problem
# flowDict contains the optimized flow
# flowCost contains the total minimized optimum
flowCost, flowDict = nx.network_simplex(G)
 
print "Optimum: %s" %flowCost  #should be 14
