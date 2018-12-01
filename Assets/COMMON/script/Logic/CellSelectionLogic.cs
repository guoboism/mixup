using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CellSelectionLogic : MonoBehaviour {

        //for games like Cell, Three Match, Add
        //core logics are all about Select several cell and do something


        public List<CellBase> selected;

        public virtual void Init() {
            selected = new List<CellBase>();
        }

        public virtual void Cancel() {
            foreach (CellBase cell in selected) {
                cell.selected = false;
                cell.SelectedChanged();
            }
            selected.Clear();
        }

        public virtual void CellAdd(CellBase cell) {
            cell.selected = true;
            cell.SelectedChanged();
            selected.Add(cell);
        }

        public virtual bool CanAdd(CellBase cell) {
            if (selected.Contains(cell)) return false;
            return true;
        }

        public virtual void CellCheck() {
        }


    }
