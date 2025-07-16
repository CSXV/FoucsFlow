import { isRouteErrorResponse, useRouteError } from "react-router-dom";

function Error() {
  const error = useRouteError();

  if (isRouteErrorResponse(error)) {
    return (
      <div>
        <h1>{error.status}</h1>
        <p>{error.statusText}</p>
      </div>
    );
  }

  if (error instanceof Error) {
    return (
      <div>
        <h1>Unexpected Error</h1>
        <p>{(error as any).message}</p>
      </div>
    );
  }

  return <p>Unknown error occurred</p>;
}

export default Error;
