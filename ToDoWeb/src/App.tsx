import "./normalize.css";
import "./App.css";
import "./loading.css";
import "./categories.css";

import {
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";

import Login, {
  loader as loginLoader,
  action as loginAction,
} from "./Components/Login";
import Account, {
  action as accountAction,
  loader as accountLoader,
} from "./Components/Account";
import Register, { action as registerAction } from "./Components/Register";
import UserNotes, { loader as allNotesLoader } from "./Components/UserNotes";

import Home from "./Components/Home";
import Error from "./Components/Error";
import Layout from "./Components/Layout";
import NotFound from "./Components/NotFound";
import NotesLayout from "./Components/NotesLayout";

// -------------------------------
const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<Layout />}>
      <Route index element={<Home />} />

      <Route
        loader={loginLoader}
        action={loginAction}
        path="login"
        element={<Login />}
        errorElement={<Error />}
      />

      <Route
        action={registerAction}
        path="register"
        element={<Register />}
        errorElement={<Error />}
      />

      <Route
        loader={accountLoader}
        action={accountAction}
        path="account/:id"
        element={<Account />}
        errorElement={<Error />}
      />

      <Route path="notes" element={<NotesLayout />}>
        <Route
          index
          element={<UserNotes />}
          loader={allNotesLoader}
          // loader={async ({ request }) => await allNotesLoader(request)}
          errorElement={<Error />}
        />
      </Route>

      <Route path="*" element={<NotFound />} />
    </Route>,
  ),
);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
