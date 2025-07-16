import { Await, useLoaderData, useSearchParams } from "react-router-dom";
import { Suspense } from "react";

import type { Note } from "../NoteType";
import { requireAuth } from "./Utils";
import { getAllNotes } from "./api";
import { Filter } from "./CategoryCard";
import Loading from "./Loading";
import NoteCard from "./NoteCard";

// -----------------------------------------------------------------------------------------------
export async function loader({ request }: any) {
  await requireAuth(request);

  return { Notes: getAllNotes(+localStorage.getItem("userID")!) };
}

// -----------------------------------------------------------------------------------------------
function UserNotes() {
  const [searchPrams, setSearchPrams] = useSearchParams();
  const typeFilter: number = +searchPrams.get("category")!;
  const allNotesData = useLoaderData();

  // ---------------------------------------------------------------------------------------------
  function handleSearch(name: number) {
    setSearchPrams({ category: name.toString() });
  }

  function renderAllNotesElements(notes: Note[]) {
    const displayFilter = typeFilter
      ? notes.filter(
        (n: Note) =>
          n.state.toLowerCase() !== "done" &&
          n.state.toLowerCase() !== "deleted" &&
          n.state.toLowerCase() !== "archived" &&
          n.categoryID === typeFilter,
      )
      : notes.filter(
        (n: Note) =>
          n.state.toLowerCase() !== "done" &&
          n.state.toLowerCase() !== "deleted" &&
          n.state.toLowerCase() !== "archived",
      );

    const allNotesElements = displayFilter.map((n: Note) => (
      <NoteCard key={n.id} note={n} />
    ));

    return (
      <>
        <section className="card-add">
          <p>Filter by:</p>

          <Filter handleSearch={handleSearch} typeFilter={typeFilter} />
        </section>

        {allNotesElements.length == 0 ? (
          <section className="card-info">
            <span className="material-symbols-outlined button">help</span>

            <h5>No notes here, add one from the button above</h5>
          </section>
        ) : (
          <section style={{ width: "100%" }}>{allNotesElements}</section>
        )}
      </>
    );
  }

  // ---------------------------------------------------------------------------------------------
  return (
    <div className="notes-container">
      <Suspense fallback={<Loading />}>
        <Await resolve={allNotesData.Notes}>{renderAllNotesElements}</Await>
      </Suspense>
    </div>
  );
}

export default UserNotes;
