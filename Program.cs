using System;
using System.Collections.Generic;

namespace ease_challenge_repo
{
    class Skiresort
    {
        private int[] row;
        private int[] col;
        private List<int> longestRoute;
        private int maxRouteScore;
        Skiresort(){
            row = new int[4] {0, 1, 0, -1};
            col = new int[4] {-1, 0, 1, 0};
            longestRoute = new List<int>();
            maxRouteScore = 0;
        }
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
        void dfsVisit(int[,] graph, List <List<int>> paths, bool[,] visited, int i, int j, List<int> path, int iT, int jT){
            visited[i,j] = true;
            path.Add(graph[i,j]);
            List<int> pathN = new List<int>(path);
            paths.Add(pathN);
            if((pathN.Count>longestRoute.Count) || (pathN.Count == longestRoute.Count && pathN[0]-pathN[pathN.Count-1] > this.maxRouteScore)){
                this.longestRoute = pathN;
                this.maxRouteScore = pathN[0]-pathN[pathN.Count-1];
            }
            for(int k=0;k<4;k++)
            {
                if(isAllowed(i, j, i+ this.row[k],j+ this.col[k], visited, graph, iT, jT)){
                    dfsVisit(graph, paths, visited, i+ this.row[k], j+ this.col[k], path, iT, jT);
                    path.RemoveAt(path.Count - 1);
                }
            }
            visited[i,j] = false;
        }

        void dfs(int iT, int jT, int[,] graph){
            bool[,] visited = new bool[iT, jT];
            List <List<int>> paths = new List <List<int>>();
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
            string[] lines = System.IO.File.ReadAllLines("map.txt");
            string[] line = lines[0].Split(" ");
            Skiresort challenge =  new Skiresort();
            int iT = Int32.Parse(line[0]);
            int jT = Int32.Parse(line[1]);
            int[,] graph = new int[iT, jT];
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
