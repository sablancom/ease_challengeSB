using System;
using System.Collections.Generic;

namespace ease_challenge_repo
{
    class Program
    {
        private int n;
        private int[] row;
        private int[] col;
        private List<int> longestRoute;
        private int maxRouteScore;
        bool isAllowed(int io, int jo, int im, int jm, bool[,] visited, int[,] graph){
            if((0 <= im && im < this.n) && (0 <= jm && jm < this.n) && !visited[im,jm]){
                if(graph[im,jm]<graph[io,jo]){
                    return true;
                }else{
                    return false;
                }
            }else{
                return false;
            }
        }
        void DFS_Visit(int[,] graph, List <List<int>> paths, bool[,] visited, int i, int j, int io, int jo,ref List<int> path){
            visited[i,j] = true;
            Console.Write(visited[i,j]+" ");
            path.Add(graph[i,j]);

            List<int> pathN = new List<int>(path);
            paths.Add(pathN);
            if(pathN.Count>this.longestRoute.Count){
                this.longestRoute = pathN;
                this.maxRouteScore = pathN[0]-pathN[pathN.Count-1];
            }else{
                if(pathN.Count == this.longestRoute.Count && pathN[0]-pathN[pathN.Count-1] > this.maxRouteScore){
                    this.longestRoute = pathN;
                    this.maxRouteScore = pathN[0]-pathN[pathN.Count-1];
                }
            }
            for(int k=0;k<4;k++)
            {
                if(isAllowed(i, j, i+ this.row[k],j+ this.col[k], visited, graph)){
                    DFS_Visit(graph, paths, visited, i+ this.row[k], j+ this.col[k], io, jo,ref path );
                    path.RemoveAt(path.Count - 1);
                }
            }
            visited[i,j] = false;
            

        }

        void DFS(int n, int[,] graph){
            bool[,] visited = new bool[n, n];
            for(int i=0;i<n;i++){
                for(int j=0;j<n;j++){
                    Console.Write(visited[i,j]+" ");
                }
                Console.WriteLine("");
            }
            List <List<int>> paths = new List <List<int>>();
            for(int i=0;i<n;i++){
                for(int j=0;j<n;j++){
                   List <int> nodeArray = new List<int>();
                    DFS_Visit(graph, paths, visited, i, j, i, j,ref nodeArray);
                }
            }
            Console.WriteLine("paths: ");
            foreach (List<int> path in paths)
            {
                path.ForEach(Console.Write);
                Console.WriteLine("");
            }
            Console.WriteLine("La ruta mas larga fue: ");
            this.longestRoute.ForEach(Console.Write);
            Console.WriteLine("Con un puntaje de: ");
            Console.WriteLine(this.maxRouteScore);
        }

        static void Main(string[] args)
        {
            int nx;//number of vertices
            Console.WriteLine("Hello World!");
            Console.WriteLine("Number of elements in x");
            nx = Int32.Parse(Console.ReadLine());
            int ny;//number of vertices
            Console.WriteLine("Number of elements in y");
            ny = Int32.Parse(Console.ReadLine());
            int[,] graph = new int[nx, ny];
            for(int i=0;i<nx;i++){
                for(int j=0;j<ny;j++){
                    Console.WriteLine("Ingrese elemento "+i+" "+j);
                    graph[i,j]=Int32.Parse(Console.ReadLine());
                }
            }
            for(int i=0;i<nx;i++){
                for(int j=0;j<ny;j++){
                    Console.Write(graph[i,j]+" ");
                }
                Console.WriteLine("");
            }
            Program challenge =  new Program();
            challenge.row = new int[4];
            challenge.col = new int[4];
            challenge.row[0] = 0;
            challenge.col[0] = -1;

            challenge.row[1] = 1;
            challenge.col[1] = 0;

            challenge.row[2] = 0;
            challenge.col[2] = 1;

            challenge.row[3] = -1;
            challenge.col[3] = 0;

            challenge.n = nx;
            challenge.longestRoute = new List<int>();
            challenge.maxRouteScore = 0;
            challenge.DFS(nx, graph);
        }
    }
}
