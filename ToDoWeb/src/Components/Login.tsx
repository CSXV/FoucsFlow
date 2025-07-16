import {
  Form,
  Link,
  redirect,
  useActionData,
  useLoaderData,
  useNavigation,
} from "react-router-dom";
import { loginUser } from "./api";
import Loading from "./Loading";

export async function action(obj: any) {
  const formData = await obj.request.formData();

  const url =
    new URL(obj.request.url).searchParams.get("redirectTo") || "/notes";

  const cred = {
    userName: formData.get("userName"),
    password: formData.get("password"),
  };

  try {
    const data = await loginUser(cred);

    localStorage.setItem("isLoggedIn", "true");
    localStorage.setItem("userID", data.id);

    return redirect(url);
  } catch (error) {
    return error.message;
  }
}

export function loader({ request }: any) {
  return new URL(request.url).searchParams.get("message");
}

function Login() {
  const message = useLoaderData();
  const errorMessage = useActionData();
  const navigate = useNavigation();

  return (
    <div>
      <h1>sign in to your account</h1>

      <div className="card-login">
        {message && <h4 className="Cancel">{message}!</h4>}

        {errorMessage && <h4 className="Cancel">{errorMessage}</h4>}
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
              name="password"
              type="password"
              placeholder="Password"
              autoComplete="current-password"
            />
          </div>

          <button
            type="submit"
            className="button Add"
            // disabled={navigate.state === "submitting"}
          >
            Login
          </button>
        </Form>
      )}

      <p>
        Don't have an account? <Link to="../register">Register now</Link>
      </p>
    </div>
    // {navigate.state === "submitting" ? "Logging in..." : "login"}
  );
}

export default Login;
