using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Itaparica.Core.Infra.Graphviz
{
    /// <summary>
    /// Componente de rastreamento de fluxo de execução.
    /// </summary>
    public class CallManager
    {
        #region Data

        private static int _id;

        public static int SequenceID
        {
            get
            {
                var id = _id;

                _id = id + 1;

                return id;
            }
        }

        private static List<Cluster> _clusters = new List<Cluster>();

        public static List<Cluster> Clusters
        {
            get
            {
                return _clusters;
            }

            set { _clusters = value; }
        }

        private static List<Call> _list = new List<Call>();

        public static List<Call> ExecutionFlow
        {
            get
            {
                return _list;
            }

            set { _list = value; }
        }

        #endregion

        public void Push(string component, string method)
        {
            var cluster = CreateOrUpdateCluster(component);

            var node = CreateOrUpdateNode(cluster, method);

            ExecutionFlow.Add(new Call { Cluster = cluster, Node = node });
        }

        private Node CreateOrUpdateNode(Cluster cluster, string method)
        {
            if (cluster.Nodes.Count(x => x.Label == method) == 0)
            {
                cluster.Nodes.Add(new Node { Id = SequenceID, Label = method });
            }

            return cluster.Nodes.Where(x => x.Label == method).Single();
        }

        private Cluster CreateOrUpdateCluster(string component)
        {
            if (Clusters.Count(x => x.Label == component) == 0)
                Clusters.Add(new Cluster { Label = component, Nodes = new List<Node>() });

            return Clusters.Where(x => x.Label == component).Single();
        }

        public string BuildGraph()
        {
            var sb = new StringBuilder();

            sb.AppendLine("digraph G {");

            for (var i = 0; i < Clusters.Count; i++)
            {
                BuildSubgraph(sb, Clusters[i], i);
            }

            BuildExecutionFlow(sb);

            sb.AppendLine("start[shape = Mdiamond];");
            sb.AppendLine("end[shape = Msquare];");

            sb.AppendLine("}");

            Clear();

            return sb.ToString();
        }

        private void Clear()
        {
            Clusters = new List<Cluster>();

            ExecutionFlow = new List<Call>();
        }

        private void BuildExecutionFlow(StringBuilder sb)
        {
            var sub = new StringBuilder();

            sub.Append("start -> ");

            foreach (var call in ExecutionFlow)
            {
                sub.Append(call.Node.Id + ";");
                sub.AppendLine("");
                sub.Append(call.Node.Id + " -> ");
            }

            sub.Append("end;");

            sb.AppendLine(sub.ToString());
        }

        private void BuildSubgraph(StringBuilder sb, Cluster cluster, int i)
        {
            sb.AppendLine("subgraph cluster_" + i + " {");
            sb.AppendLine("style=filled;");
            sb.AppendLine("color=" + TranslateColor(cluster.Label) + ";");
            sb.AppendLine("node [style=filled,color=white];");
            sb.AppendLine("");

            foreach (var node in cluster.Nodes)
            {
                sb.AppendLine("\"" + node.Id + "\" [label = \"" + node.Label + "\"];");
            }

            sb.AppendLine("label = \"" + cluster.Label + "\";");

            sb.AppendLine("}");
        }

        private string TranslateColor(string component)
        {
            if (component.EndsWith("Controller"))
                return "lightblue";

            if (component.EndsWith("Service"))
                return "lightsalmon";

            if (component.EndsWith("Repository"))
                return "lightgreen";


            return "lightgrey";
        }
    }

    public class Cluster
    {
        public string Label { get; set; }

        public List<Node> Nodes { get; set; }
    }

    public class Node
    {
        public int Id { get; set; }

        public string Label { get; set; }
    }

    public class Call
    {
        public Cluster Cluster { get; set; }

        public Node Node { get; set; }
    }
}

