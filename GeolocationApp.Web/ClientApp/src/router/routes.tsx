import {createBrowserRouter} from "react-router-dom";
import HomeRouter from "@routes/home/HomeRouter.ts";
import NotFoundPage from "@pages/NotFoundPage.tsx";

const routes = createBrowserRouter([
    ...HomeRouter,
    {
        id: "not-found",
        path: "*",
        Component: NotFoundPage,
    },
]);

export default routes;