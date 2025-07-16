import { useEffect, useState } from "react";
import { getAllCategories } from "./api";
import type { Category } from "../NoteType";

type FilterProps = {
  handleSearch: (searchTerm: number) => void;
  typeFilter: number | null;
};

//this is the worst component of the program :(
async function loader() {
  const f = await getAllCategories();
  return f;
}

function CategoryCard({
  getCategory,
  setCat,
}: {
  getCategory: (param: number) => void;
  setCat?: number | null;
}) {
  const [categories, setCategories] = useState<[Category]>();
  const [selectedCat, setSelectedCat] = useState(
    setCat === undefined ? 6 : setCat,
  );

  useEffect(() => {
    loader().then((data) => setCategories(data));
  }, []);

  function handleClick(e: number) {
    setSelectedCat(e);
    getCategory(e);
  }

  let cat = null;

  if (categories != null) {
    cat = categories.map((c: Category) => (
      <button
        key={c.id}
        className={`button ${c.name} ${selectedCat == c.id ? c.name + "Click" : ""}`}
        onClick={() => handleClick(c.id)}
        type="button"
      >
        <span className="material-symbols-outlined">{c.iconName}</span>
        {c.name}
      </button>
    ));
  }

  return <div className="card">{cat}</div>;
}

// --------------------------------------------------------------------
export function Filter({ handleSearch, typeFilter }: FilterProps) {
  const [categories, setCategories] = useState<[Category]>();
  const [filter, setFilter] = useState(false);

  useEffect(() => {
    loader().then((data) => setCategories(data));
  }, []);

  function clearFilter() {
    setFilter(!filter);
    handleSearch(0);
  }

  let cat = null;

  if (categories != null) {
    cat = categories.map((c: Category) => (
      <button
        key={c.id}
        className={`button ${c.name} ${typeFilter == c.id ? c.name + "Click" : ""}`}
        type="button"
        onClick={() => {
          handleSearch(c.id);
          setFilter(true);
        }}
      >
        <span className="material-symbols-outlined">{c.iconName}</span>
        {c.name}
      </button>
    ));
  }

  return (
    <div className="card">
      {cat}

      {filter ? (
        <button className="button Add" onClick={clearFilter}>
          <span className="material-symbols-outlined">search_off</span>
          Clear
        </button>
      ) : null}
    </div>
  );
}

export default CategoryCard;
