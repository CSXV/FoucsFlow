import { useState } from "react";
import { Outlet } from "react-router-dom";
import AddNote from "./AddNote";

function NotesLayout() {
  const [addMenu, setAddMenu] = useState<boolean>(false);

  function openAddMenu() {
    setAddMenu(!addMenu);
  }

  return (
    <>
      {addMenu ? (
        <AddNote setAddMenu={setAddMenu} mode={0} />
      ) : (
        <div className="card-add-button">
          <a onClick={openAddMenu} className="button Add">
            <span className="material-symbols-outlined">note_add</span>
            Add new note
          </a>
        </div>
      )}

      <Outlet />
    </>
  );
}

export default NotesLayout;
