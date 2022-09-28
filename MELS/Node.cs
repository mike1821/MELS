using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*! \namespace AnimalChange */

namespace AnimalChange
{
    /*! A class that named Node. contains a node in the xml tree. */
    public class Node
    {
        //name of that node
        private string nodeName;
        //value of that note
        private string nodeValue;
        //file where it was found
        public string FileName;
        //all children of that node
        public List<Node> SubNode;
        //! A constructor.
        /*!
         without argument.
        */

        public Node()
        {
            SubNode = new List<Node>();
        }
        //addding a child to that node
        //! A normal member, Add Child. Taking two arguments.
        /*!
          \param name, a string argument.
          \param value, a string argument.
        */
        public void addChild(string name, string value)
        {
            Node newNode = new Node();
            newNode.nodeName = name;
            newNode.setNodeValue(value);
            SubNode.Add(newNode);
        }
        //addding a child to that node
        //! A normal member, Add Child. Taking one argument.
        /*!
          \param newNode, a instance argument that points to class Node.
        */
        public void addChild(Node newNode)
        {
            SubNode.Add(newNode);
        }
        //! A normal member, Set Node Name. Taking one argument.
        /*!
          \param name, a string argument.
        */
        public void setNodeName(string name)
        {
            nodeName = name;
        }
        //! A normal member, Get Node Name. Returning one value.
        /*!
          \return a string value.
        */
        public string getNodeName()
        {
           return  nodeName;
        }
        //! A normal member, Set Node Value. Taking one argument.
        /*!
          \param value, a string argument.
        */
        public void setNodeValue(string value)
        {
            nodeValue = value;
        }
        //! A normal member, Get Node Value. Returning one value.
        /*!
          \return a string value.
        */
        public string getNodeValue()
        {
            return nodeValue;
        }
        //! A normal member, Get Number of Nodes. Returning one value.
        /*!
          \return an integer value.
        */
        public int getNumberOfNodes()
        {
            return SubNode.Count();
        }
    }
}
