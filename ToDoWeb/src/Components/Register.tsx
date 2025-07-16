import { Form, redirect, useActionData, useNavigation } from "react-router-dom";
import Loading from "./Loading";
import { registerNewUser } from "./api";
import type { registerCreds } from "../NoteType";

export async function action(obj: any) {
  const formData = await obj.request.formData();

  if (formData.get("password") !== formData.get("confirmPassword")) return;

  const url =
    new URL(obj.request.url).searchParams.get("redirectTo") || "/notes";

  const cred: registerCreds = {
    userName: formData.get("userName"),
    email: formData.get("email"),
    password: formData.get("password"),
    firstName: formData.get("firstName"),
    lastName: formData.get("lastName"),
    isActive: true,
  };

  try {
    const data = await registerNewUser(cred);

    localStorage.setItem("isLoggedIn", "true");
    localStorage.setItem("userID", data.id);

    return redirect(url);
  } catch (error) {
    // return (error as any).message;

    if (error instanceof Error) {
      return error.message;
    }
  }
}

function Register() {
  const navigate = useNavigation();
  const errorMessage = useActionData();

  return (
    <div>
      <h2>Wellcome new user</h2>

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
            />

            <input
              className="button input"
              name="email"
              type="email"
              placeholder="Email"
              autoComplete="email"
            />

            <br />
            <input
              className="button input"
              name="firstName"
              type="text"
              placeholder="First name"
              autoComplete="name"
            />

            <input
              className="button input"
              name="lastName"
              type="text"
              placeholder="Last name"
              autoComplete="name"
            />

            <br />
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
            Register
          </button>
        </Form>
      )}
    </div>
  );
}

export default Register;
