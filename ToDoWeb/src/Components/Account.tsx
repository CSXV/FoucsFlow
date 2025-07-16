import {
  Await,
  Form,
  redirect,
  useActionData,
  useLoaderData,
  useNavigation,
} from "react-router-dom";
import Loading from "./Loading";
import { requireAuth } from "./Utils";
import { deleteUserByID, getUserByID, loginUser, updateUserByID } from "./api";
import type { User } from "../NoteType";
import { Suspense } from "react";

export async function loader({ params, request }: any) {
  await requireAuth(request);

  return { user: getUserByID(params.id) };
}

export async function action(obj: any) {
  const formData = await obj.request.formData();
  if (formData.get("password") !== formData.get("confirmPassword")) return;

  const url =
    new URL(obj.request.url).searchParams.get("redirectTo") || "/notes";

  const date = new Date();

  const updatedUser: User = {
    id: +localStorage.getItem("userID")!,

    userName: formData.get("userName"),
    email: formData.get("email"),
    password: formData.get("password"),

    firstName: formData.get("firstName"),
    lastName: formData.get("lastName"),

    createDate: date.toISOString(),
    updateDate: date.toISOString(),

    profileImage: "",
    userType: 0,
    isActive: true,
  };

  const cred = {
    userName: formData.get("userName"),
    password: formData.get("password"),
  };

  const login = await loginUser(cred);
  if (login === null) return;

  try {
    const data = await updateUserByID(updatedUser);
    if (data === null) return;

    return redirect(url);
  } catch (error: any) {
    return error.message;
  }
}

function Account() {
  const userData = useLoaderData();
  const navigate = useNavigation();
  const errorMessage = useActionData();

  function handleAccountDeletion() {
    deleteUserByID(+localStorage.getItem("userID")!);

    localStorage.setItem("userID", "0");
    localStorage.setItem("isLoggedIn", "false");

    return redirect("/");
  }

  function renderUserEditElements(user: User) {
    return (
      <div>
        <h2>Update your info</h2>

        <div className="card-login">
          {errorMessage && <h3 className="button Cancel">{errorMessage}</h3>}
        </div>

        {navigate.state === "submitting" ? (
          <Loading />
        ) : (
          <Form className="card-login" method="post" replace>
            <div className="input-container">
              <input
                className="button input"
                name="userName"
                type="text"
                placeholder="User name"
                autoComplete="username"
                value={user.userName}
              />

              <input
                className="button input"
                name="email"
                type="email"
                placeholder="Email"
                autoComplete="email"
                defaultValue={user.email}
              />

              <br />
              <input
                className="button input"
                name="firstName"
                type="text"
                placeholder="First name"
                autoComplete="name"
                defaultValue={user.firstName}
              />

              <input
                className="button input"
                name="lastName"
                type="text"
                placeholder="Last name"
                autoComplete="name"
                defaultValue={user.lastName}
              />

              <br />
              <p>Check your password</p>

              <input
                className="button input"
                name="password"
                type="password"
                placeholder="Password"
                autoComplete="current-password"
              />

              <input
                className="button input"
                name="confirmPassword"
                type="password"
                placeholder="Confirm password"
              />
            </div>

            <button type="submit" className="button Add">
              Update
            </button>
          </Form>
        )}

        <div className="card-login">
          <button
            onClick={handleAccountDeletion}
            type="button"
            className="button Cancel"
          >
            Delete account
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="notes-container">
      <Suspense fallback={<Loading />}>
        <Await resolve={userData.user}>{renderUserEditElements}</Await>
      </Suspense>
    </div>
  );
}

export default Account;
