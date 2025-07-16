import { useRouteError } from "react-router-dom";

function Error() {
  const error = useRouteError();

  return (
    <div>
      <h2>Error: {error.message}</h2>
      <pre>{error.statusText}</pre>
      <pre>{error.status}</pre>
    </div>
  );
}

export default Error;
