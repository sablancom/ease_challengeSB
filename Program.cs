using System;
using System.Collections.Generic;

namespace ease_challenge_repo
{
    /// <summary>Class <c>Skiresort</c> implements the logic to calculate the longest and steepest path from the input file.</summary>
    class Skiresort
    {
        private int[] row;
        private int[] col;
        private List<int> longestRoute;
        private int maxRouteScore;

        /// <summary>By default the constructor initialice the row and col arrays, the array of the longest route and the variable of the
        ///maximum score. The col and row arrays includes the four possible movementes of a cell (top, right, bottom, left)</summary>
        Skiresort(){
            row = new int[4] {0, 1, 0, -1};
            col = new int[4] {-1, 0, 1, 0};
            longestRoute = new List<int>();
            maxRouteScore = 0;
        }
        /// <summary>Method to check if it is allowed to go to cell (im, jm) from the current cell (io, jo).
        /// The method returns false if the cell (im, jm) is not a valid coordinate, if it was already visited or the value 
        ///of the new coordinate is bigger than the original coordinate.
        /// </summary>
        bool isAllowed(int io, int jo, int im, int jm, bool[,] visited, int[,] graph, int iT, int jT){
            if((0 <= im && im < iT) && (0 <= jm && jm < jT) && !visited[im,jm]){
                if(graph[im,jm]<graph[io,jo]){
                    return true;
                }else{
                    return false;
                }
            }else{
                return false;
            }
        }
        /// <summary>Recursive method of the DFS Algorithm used to generate all the possible routes from a starting coordinate.
        /// </summary>
        void dfsVisit(int[,] graph, List <List<int>> paths, bool[,] visited, int i, int j, List<int> path, int iT, int jT){
            // the current element gets marked as visited
            visited[i,j] = true;
            //the element is added to the path
            path.Add(graph[i,j]);
            List<int> pathN = new List<int>(path);
            //the path is added to the main paths List
            paths.Add(pathN);
            //if the path has a bigger size and drop, the longestRoute and maxRouteScore variables are updated.
            if((pathN.Count>longestRoute.Count) || (pathN.Count == longestRoute.Count && pathN[0]-pathN[pathN.Count-1] > this.maxRouteScore)){
                this.longestRoute = pathN;
                this.maxRouteScore = pathN[0]-pathN[pathN.Count-1];
            }
            // The dfsVisit checks the four possible movements from the current cell
            for(int k=0;k<4;k++)
            {
                //the new possible movement is skipped if is invalid or was already visited.
                if(isAllowed(i, j, i+ this.row[k],j+ this.col[k], visited, graph, iT, jT)){
                    dfsVisit(graph, paths, visited, i+ this.row[k], j+ this.col[k], path, iT, jT);
                    //after processing the new node, the last element in path is deleted.
                    path.RemoveAt(path.Count - 1);
                }
            }
            //the node is marked as not visited at the end of the method. 
            visited[i,j] = false;
        }
        /// <summary> Main method of the DFS Algorithm.</summary>
        void dfs(int iT, int jT, int[,] graph){
            //The visited array indicates if a cell was visited or not.
            bool[,] visited = new bool[iT, jT];
            //List containig all the posible paths calculated.
            List <List<int>> paths = new List <List<int>>();
            //for each element in graph the dfsVisit gets called generating all the possible routes from that element.
            for(int i=0;i<iT;i++){
                for(int j=0;j<jT;j++){
                   List <int> path = new List<int>();
                    dfsVisit(graph, paths, visited, i, j, path, iT, jT);
                }
            }
            Console.WriteLine("The longest and steepest calculated path is: ");
            this.longestRoute.ForEach(i => Console.Write("{0} -> ", i));
            Console.WriteLine("");
            Console.WriteLine("The length of the calculated path is: ");
            Console.WriteLine(this.longestRoute.Count);
            Console.WriteLine("The drop of the calculated path is: ");
            Console.WriteLine(this.maxRouteScore);
        }

        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            //The main class is initialized
            Skiresort challenge =  new Skiresort();
            //The file is readed and the content is stored
            string[] lines = System.IO.File.ReadAllLines("map.txt");
            string[] line = lines[0].Split(" ");
            //The number of rows and columns is saved from the first line. The matrix graph is initilized.
            int iT = Int32.Parse(line[0]);
            int jT = Int32.Parse(line[1]);
            int[,] graph = new int[iT, jT];
            //Each element in the file is stored in the matrix.
            for(int i=0;i<iT;i++){
                line = lines[i+1].Split(" ");
                for(int j=0;j<jT;j++){
                    graph[i,j] = Int32.Parse(line[j]);
                }
            }
            challenge.dfs(iT, jT, graph);
            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);
            Console.WriteLine("Elapsed Time is {0} ms", ts.TotalMilliseconds);
        }
    }
}
