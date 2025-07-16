import { useState } from "react";
import type { Note } from "../NoteType";
import { updateNoteByID } from "./api";
import AddNote from "./AddNote";

function NoteCard({ note }: { note: Note }) {
  const [editButtons, setEditButtons] = useState({ state: false, id: 0 });
  const [edit, setEdit] = useState({ state: false, id: 0 });
  const [hideNote, setHideNote] = useState(false);

  const dateObj = new Date(note.updateDate);

  function handleNoteClick(data: Note, state: string) {
    data.state = state;

    updateNoteByID(data);
    setHideNote(true);
  }

  function handleNoteEdit(data: Note) {
    setEdit((prev) => ({ id: data.id, state: !prev.state }));
  }

  function handleEditMenu(id: number) {
    setEditButtons((prev) => ({ id: id, state: !prev.state }));
  }

  return (
    <section
      className="input-container"
      style={{ display: !hideNote ? "flex" : "none" }}
    >
      <div className="note-link">
        <div>
          <h4>{note.title}</h4>
          <p>{note.content}</p>
        </div>

        <div className="card-right">
          <p className="">{dateObj.toLocaleDateString()}</p>

          <div style={{ display: "flex" }}>
            {editButtons.state && editButtons.id === note.id ? (
              <>
                <button
                  className="button Add"
                  onClick={() => handleNoteEdit(note)}
                >
                  <span className="material-symbols-outlined">edit</span>
                </button>

                <button
                  className="button Add"
                  onClick={() => handleNoteClick(note, "Archived")}
                >
                  <span className="material-symbols-outlined">archive</span>
                </button>

                <button
                  className="button Cancel"
                  onClick={() => handleNoteClick(note, "Deleted")}
                >
                  <span className="material-symbols-outlined">delete</span>
                </button>
              </>
            ) : null}

            {editButtons.state && editButtons.id === note.id ? (
              <button
                className="button Add"
                onClick={() => handleEditMenu(note.id)}
              >
                <span className="material-symbols-outlined">close</span>
              </button>
            ) : (
              <button
                className="button Add"
                onClick={() => handleEditMenu(note.id)}
              >
                <span className="material-symbols-outlined">more_horiz</span>
              </button>
            )}

            <button
              onClick={() => {
                handleNoteClick(note, "Done");
              }}
              className="button Done"
            >
              <span className="material-symbols-outlined">done_outline</span>
            </button>
          </div>
        </div>
      </div>

      {edit.state && edit.id == note.id && editButtons.state ? (
        <section>
          <AddNote
            setAddMenu={() => handleNoteEdit(note)}
            mode={1}
            noteData={note}
          />
        </section>
      ) : null}
    </section>
  );
}

export default NoteCard;
