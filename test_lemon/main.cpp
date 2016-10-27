// c++ STL containers
#include <vector>
#include <map>
#include <string>
// LEMON
#include "lemon/smart_graph.h"
#include "lemon/dijkstra.h"

struct Arc
{
    std::string sourceID;
    std::string targetID;
    double cost;
};

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
    //instantiate directed smart graph
    lemon::SmartDigraph g;
    //instantiate a LEMON map of arc costs
    lemon::SmartDigraph::ArcMap<double> costMap(g);

    lemon::SmartDigraph::NodeMap<std::string> nodeMap(g);

    std::map< std::string, int > nodes = { std::make_pair("A",0),
                                 std::make_pair("B",1),
                                 std::make_pair("C",2),
                                 std::make_pair("D",3),
                                 std::make_pair("E",4),
                                 std::make_pair("F",5)
                               };

    std::vector<Arc> arcs = {    Arc {"A","B",4},
                            Arc {"A","C",2},
                            Arc {"B","D",10},
                            Arc {"B","C",5},
                            Arc {"C","E",3},
                            Arc {"E","D",4},
                            Arc {"D","F",11}
                        };

    // defining the type of the Dijkstra Class
    using SptSolver = lemon::Dijkstra<lemon::SmartDigraph, lemon::SmartDigraph::ArcMap<double>>;

    //populate graph
    //nodes first
    lemon::SmartDigraph::Node currentNode;
    for (auto nodesIter = nodes.begin(); nodesIter != nodes.end(); ++nodesIter)
    {
        std::string key = nodesIter->first;
        currentNode = g.addNode();
        nodeMap[currentNode] = key;
    }
    //then the arcs with the costs through the cost map
    lemon::SmartDigraph::Arc currentArc;
    for (auto arcsIter = arcs.begin(); arcsIter != arcs.end(); ++arcsIter)
    {
        int sourceIndex = nodes.at(arcsIter->sourceID);
        int targetIndex = nodes.at(arcsIter->targetID);

        lemon::SmartDigraph::Node sourceNode = g.nodeFromId(sourceIndex);
        lemon::SmartDigraph::Node targetNode = g.nodeFromId(targetIndex);

        currentArc = g.addArc(sourceNode, targetNode);
        costMap[currentArc] = arcsIter->cost;
    }

    //add source & target
    lemon::SmartDigraph::Node startN = g.nodeFromId( nodes.at("A") );
    lemon::SmartDigraph::Node endN = g.nodeFromId( nodes.at("E") );

    SptSolver spt (g, costMap);
    spt.run(startN, endN);

    /* Walk in whole SPT is possible from specified orig and end
       but dest must be part of the SPT and
       an orig node must not be a dest node */
    std::vector<lemon::SmartDigraph::Node> path;
    for (lemon::SmartDigraph::Node v = endN; v != startN; v = spt.predNode(v))
    {
      if (v != lemon::INVALID && spt.reached(v)) //special LEMON node constant
      {
         path.push_back(v);
      }
    }
    path.push_back(startN);

    double cost = spt.dist(endN);

    //print out the path with reverse iterator
    std::cout << "Path from " << " to " << " is: " << std::endl;
    for (auto p = path.rbegin(); p != path.rend(); ++p)
        std::cout << nodeMap[*p] << std::endl;
    std::cout << "Total cost for the shortest path is: "<< cost << std::endl;

    return EXIT_SUCCESS;
}
