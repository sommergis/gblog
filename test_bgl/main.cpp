// c++ STL
#include <vector>
#include <map>
#include <string>
#include <iostream>
// BGL
#include "boost/graph/adjacency_list.hpp"
#include "boost/graph/dijkstra_shortest_paths.hpp"

struct Arc
{
    std::string sourceID;
    std::string targetID;
    double cost;
};

//typedef of a templated directed adjacency_list
// with std::vector-Containers for nodes and arcs (=vecS)
// the nodes do not have any special properties (-> boost::no_property)
// while the arcs are defined with a bundled property
// (see http://www.boost.org/doc/libs/1_62_0/libs/graph/doc/bundles.html)
typedef boost::adjacency_list< boost::vecS, boost::vecS, boost::directedS,
                        boost::no_property,
                        Arc > graph_t;
//typedef of the vertex_descriptor depends of the defined graph type
typedef boost::graph_traits<graph_t>::vertex_descriptor vertex_descriptor;

int main()
{

/*
Graph
    A---4-->B--10-->D--11-->F
    |       |       ^
    |       5       4
    |       v       |
    ---2--->C---3-->E

*/
    //instantiate a directed graph
    graph_t g;

    std::map< std::string, int > nodes = { std::make_pair("A",0),
                                 std::make_pair("B",1),
                                 std::make_pair("C",2),
                                 std::make_pair("D",3),
                                 std::make_pair("E",4),
                                 std::make_pair("F",5)
                               };

    std::vector<Arc> arcs = {    Arc {"A","B",4},
                            Arc {"A","C",2},
                            Arc {"B","C",5},
                            Arc {"B","D",10},
                            Arc {"C","E",3},
                            Arc {"D","F",11},
                            Arc {"E","D",4}
                        };

    //populate graph

    //nodes first
    /*for (int i = 0; i < nodes.size(); i++)
    {
        //returns vertex_descriptor
        auto vertex = boost::add_vertex(g);
    }*/

    //then with arcs and the costs
    for (auto arcsIter = arcs.begin(); arcsIter != arcs.end(); ++arcsIter)
    {
        int srcID = nodes.at(arcsIter->sourceID);
        int tgtID = nodes.at(arcsIter->targetID);
        //add_edge() returns std::pair<edge_descriptor,bool>
        auto tmpArc = boost::add_edge(srcID, tgtID, g);
        //add costs to arcs
        g[tmpArc.first].cost = arcsIter->cost;
    }

    //add source & target
    vertex_descriptor startV = boost::vertex( nodes.at("A"), g );
    vertex_descriptor endV = boost::vertex( nodes.at("E"), g );

    //predecessors
    // Output for predecessors of each node in the shortest path tree result
    std::vector<vertex_descriptor> predMap(boost::num_vertices(g));

    //distMap
    // Output for distances for each node with initial size
    // of number of vertices
    std::vector<double> distMap(boost::num_vertices(g));

    //solve shortest path problem
    boost::dijkstra_shortest_paths(g, startV,
      weight_map(boost::get(&Arc::cost, g)) //arc costs from bundled properties
      .predecessor_map(boost::make_iterator_property_map(predMap.begin(),//property map style
                                                        boost::get(boost::vertex_index, g)))
      .distance_map(boost::make_iterator_property_map(distMap.begin(),//property map style
                                                        boost::get(boost::vertex_index, g)))
      );

    /* Walk in whole SPT is possible from specified orig and end
       but dest must be part of the SPT and
       an orig node must not be a dest node */
    std::vector<vertex_descriptor> path;
    vertex_descriptor current = endV;
    while (startV != current)
    {
        path.push_back(current);
        current = predMap[current];
    }
    // add start as last element (=start node) to path
    path.push_back(startV);

    //iterate over path elements in reverse oder to
    // construct to normal ordered path
    std::vector<vertex_descriptor>::reverse_iterator rit;
    std::cout <<"Path from "<< startV << " to "<< endV << " is: "<< std::endl;
    double totalCost = distMap[endV];
    for (rit = path.rbegin(); rit != path.rend(); ++rit)
        std::cout << *rit << " -> ";

    std::cout << std::endl;
    std::cout << "Total Cost: "<< totalCost << std::endl;

    return EXIT_SUCCESS;
}

