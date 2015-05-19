//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using J2Net.Grammar;
//using Antlr4.Runtime;
//using Antlr4.Runtime.Tree;
//using Antlr4.Runtime.Misc;
//using System.IO;
//using System.Diagnostics;


//namespace J2Net
//{
//    public class TreePrinterListener : IParseTreeListener
//    {
//        private List<String> ruleNames;
//        private StringBuilder builder = new StringBuilder();

//        public TreePrinterListener(Parser parser)
//        {
//            // this.ruleNames = Arrays.asList(parser.getRuleNames());
//        }

//        public TreePrinterListener(List<String> ruleNames)
//        {
//            this.ruleNames = ruleNames;
//        }

//        public void EnterEveryRule(ParserRuleContext ctx)
//        {
//            if (builder.Length > 0)
//            {
//                builder.Append(' ');
//            }

//            if (ctx.ChildCount > 0)
//            {
//                builder.Append('(');
//            }

//            int ruleIndex = ctx.GetRuleIndex();
//            String ruleName;
//            if (ruleIndex >= 0 && ruleIndex < ruleNames.size())
//            {
//                ruleName = ruleNames.get(ruleIndex);
//            }
//            else
//            {
//                ruleName = Integer.toString(ruleIndex);
//            }

//            builder.Append(ruleName);
//        }

//        public void ExitEveryRule(ParserRuleContext ctx)
//        {
//            throw new NotImplementedException();
//        }

//        public void VisitErrorNode(IErrorNode node)
//        {
//            if (builder.Length > 0)
//            {
//                builder.Append(' ');
//            }

//            builder.Append(Utils.EscapeWhitespace(Trees.GetNodeText(node, ruleNames), false));
//        }

//        public void VisitTerminal(ITerminalNode node)
//        {
//            if (builder.Length > 0)
//            {
//                builder.Append(' ');
//            }

//            builder.Append(Utils.EscapeWhitespace(Trees.GetNodeText(node, ruleNames), false));

//        }

//        public String toString()
//        {
//            return builder.ToString();
//        }
//    }
//}
