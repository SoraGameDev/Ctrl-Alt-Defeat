using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Fungus.EditorUtils
{
    /// <summary>
    /// Show the variable selection window as a searchable popup
    /// </summary>
    public class VariableSelectPopupWindowContent : BasePopupWindowContent
    {
        static readonly int POPUP_WIDTH = 200, POPUP_HEIGHT = 200;
        static List<System.Type> types;

        static void CacheVariableTypes()
        {
            var derivedType = typeof(Variable);
            types = EditorExtensions.FindDerivedTypes(derivedType)
                .Where(x => !x.IsAbstract && derivedType.IsAssignableFrom(x))
                .ToList();
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            CacheVariableTypes();
        }

        protected override void PrepareAllItems()
        {
            if(types == null || types.Count == 0)
            {
                CacheVariableTypes();
            }

            int i = 0;
            foreach (var item in types)
            {
                VariableInfoAttribute variableInfo = VariableEditor.GetVariableInfo(item);
                if (variableInfo != null)
                {
                    allItems.Add(new FilteredListItem(i, (variableInfo.Category.Length > 0 ? variableInfo.Category + CATEGORY_CHAR : "") + variableInfo.VariableType));
                }

                i++;
            }
        }

        protected override void SelectByOrigIndex(int index)
        {
            AddVariable(types[index]);
        }

        static public void DoAddVariable(Rect position, string currentHandlerName, Flowchart flowchart)
        {
            curFlowchart = flowchart;
            if (FungusEditorPreferences.useExperimentalMenus)
            {
                //new method
                VariableSelectPopupWindowContent win = new VariableSelectPopupWindowContent(currentHandlerName, POPUP_WIDTH, POPUP_HEIGHT);
                PopupWindow.Show(position, win);
            }
            //old method
            DoOlderMenu(flowchart);
        }

        static protected void DoOlderMenu(Flowchart flowchart)
        {
            GenericMenu menu = new GenericMenu();

            // Add variable types without a category
            foreach (var type in types)
            {
                VariableInfoAttribute variableInfo = VariableEditor.GetVariableInfo(type);
                if (variableInfo == null ||
                    variableInfo.Category != "")
                {
                    continue;
                }

                GUIContent typeName = new GUIContent(variableInfo.VariableType);

                menu.AddItem(typeName, false, AddVariable, type);
            }

            // Add types with a category
            foreach (var type in types)
            {
                VariableInfoAttribute variableInfo = VariableEditor.GetVariableInfo(type);
                if (variableInfo == null ||
                    variableInfo.Category == "")
                {
                    continue;
                }
                
                GUIContent typeName = new GUIContent(variableInfo.Category + CATEGORY_CHAR + variableInfo.VariableType);

                menu.AddItem(typeName, false, AddVariable, type);
            }

            menu.ShowAsContext();
        }

        private static Flowchart curFlowchart;

        public VariableSelectPopupWindowContent(string currentHandlerName, int width, int height)
            : base(currentHandlerName, width, height)
        {
        }

        protected static void AddVariable(object obj)
        {
            System.Type t = obj as System.Type;
            if (t == null)
            {
                return;
            }

            Undo.RecordObject(curFlowchart, "Add Variable");
            Variable newVariable = curFlowchart.gameObject.AddComponent(t) as Variable;
            newVariable.Key = curFlowchart.GetUniqueVariableKey("");
            curFlowchart.Variables.Add(newVariable);

            // Because this is an async call, we need to force prefab instances to record changes
            PrefabUtility.RecordPrefabInstancePropertyModifications(curFlowchart);
        }
    }
}