import {RouteObject} from "react-router-dom";
import AppLayout from "@layouts/AppLayout.tsx";
import HomePage from "@pages/HomePage.tsx";
import VisitPage from "@pages/VisitPage.tsx";
import ListVisitPage from "@pages/ListVisitPage.tsx";

const HomeRouter: RouteObject[] = [
    {
        id: "home",
        path: "/",
        Component: AppLayout,
        children: [
            {
                index: true,
                Component: HomePage,
            },
            {
                path: 'visit',
                Component: ListVisitPage,
            },
            {
                path: 'visit/:id',
                Component: VisitPage,
            },
            
        ],
    },
];

export default HomeRouter