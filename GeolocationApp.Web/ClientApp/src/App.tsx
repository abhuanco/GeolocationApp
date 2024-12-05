import './App.css'
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import routes from "@routes/routes.tsx";
import {RouterProvider} from "react-router-dom";

function App() {

    return (
        <>
            <RouterProvider router={routes}/>
        </>
    )
}

export default App
