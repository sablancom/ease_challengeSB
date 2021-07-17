using System;
using System.Collections.Generic;

namespace ease_challenge_repo
{
    class Program
    {
        void DFS_Visit(List<int[]> paths, bool[,] visited, int i, int j){
            visited[i,j] = true;
            Console.Write(visited[i,j]+" ");
            //....
        }

        void DFS(int n){
            bool[,] visited = new bool[n, n];
            for(int i=0;i<n;i++){
                for(int j=0;j<n;j++){
                    Console.Write(visited[i,j]+" ");
                }
                Console.WriteLine("");
            }
            List<int[]> paths = new List<int[]>();
            for(int i=0;i<n;i++){
                for(int j=0;j<n;j++){
                    DFS_Visit(paths, visited, i, j);
                }
            }
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
            challenge.DFS(nx);
        }
    }
}
