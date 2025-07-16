import { Form } from "react-router-dom";
import CategoryCard from "./CategoryCard";
import { addNewNote, updateNoteByID } from "./api";
import { useState } from "react";
import type { Note } from "../NoteType";

function AddNote({
  setAddMenu,
  mode,
  noteData,
}: {
  setAddMenu: (prop: boolean) => void;
  mode: number;
  noteData?: Note;
}) {
  const [selectedCategory, setSelectedCategory] = useState<number>(0);
  const [message, setMessage] = useState(false);

  function getCategory(cat: number) {
    setSelectedCategory(cat);
  }

  function handleEditing(e: any) {
    e.preventDefault();

    const { title, content } = e.target;

    if (title.value === "" || content.value === "" || selectedCategory === 0) {
      setMessage(true);
      return;
    }

    const date = new Date();
    const id = +localStorage.getItem("userID")!;

    const editedNote: Note = {
      id: noteData!.id,
      userID: id,
      categoryID: selectedCategory,

      title: title.value,
      content: content.value,

      createDate: date.toISOString(),
      updateDate: date.toISOString(),

      state: "to do",
      isPinned: false,
    };

    setMessage(false);
    updateNoteByID(editedNote);
  }

  function handleAdding(e: any) {
    e.preventDefault();

    const { title, content } = e.target;

    if (title.value === "" || content.value === "" || selectedCategory === 0) {
      setMessage(true);
      return;
    }

    const date = new Date();
    const id = +localStorage.getItem("userID")!;

    const newNote: Note = {
      id: 0,
      userID: id,
      categoryID: selectedCategory,

      title: title.value,
      content: content.value,

      createDate: date.toISOString(),
      updateDate: date.toISOString(),

      state: "to do",
      isPinned: false,
    };

    setMessage(false);
    addNewNote(newNote);
  }

  return (
    <Form
      className="card-container"
      method="post"
      onSubmit={mode == 0 ? handleAdding : handleEditing}
    >
      <div className="input-container">
        <input
          name="title"
          className="button input"
          type="text"
          placeholder="Note title"
          defaultValue={mode == 1 ? noteData?.title : ""}
        ></input>

        <textarea
          name="content"
          className="button"
          placeholder="Note content"
          defaultValue={mode == 1 ? noteData?.content : ""}
        ></textarea>
      </div>

      <CategoryCard getCategory={getCategory} setCat={noteData?.categoryID} />

      <div className="card-button">
        <button className="button Add" type="submit">
          <span className="material-symbols-outlined">
            {mode === 0 ? "note_add" : "edit_note"}
          </span>
          {mode === 0 ? "Add new note" : "Edit this note"}
        </button>

        <button
          type="reset"
          className="button Cancel"
          onClick={() => setAddMenu(false)}
        >
          <span className="material-symbols-outlined">cancel</span>
          Cancel
        </button>
      </div>

      {message ? <p>Please fill the empty fileds</p> : null}
    </Form>
  );
}

export default AddNote;
