using System.Collections.Generic;

namespace APIrest.HATEOAS
{
    public class HATEOAS
    {
        public HATEOAS(string url){
            this.url = url;
        }

        public HATEOAS(string url, string protocol){
            this.url = url;
            this.protocol = protocol;
        }


        private string url;
        private string protocol = "https://";
        public List<Link> actions = new List<Link>();


        public void AddAction(string rel, string method){
            // https://localhost:5001/api/v1/Produtos
            actions.Add(new Link(this.protocol + this.url, rel, method));
        }

        
        public Link[] GetActions(string sufix){
            Link[] tempLinks = new Link[actions.Count];

            for(int i = 0; i < tempLinks.Length; i++){
                tempLinks[i] = new Link(actions[i].href, actions[i].rel, actions[i].method);
            }

            /* Montagem do link */
            foreach(var link in tempLinks){
                // https://localhost:5001/api/v1/Produtos/
                link.href = link.href + "/" + sufix;
            }
            return tempLinks;
        }
    }
}