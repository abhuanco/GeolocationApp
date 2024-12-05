import React from "react";
import {Outlet} from "react-router-dom";
import {Box, Container, CssBaseline} from "@mui/material";
import NavbarComponent from "@components/NavbarComponent.tsx";

export default function AppLayout(): React.JSX.Element {
    return (
        <React.Fragment>
            <CssBaseline/>
            <Container maxWidth="xl">
                <NavbarComponent/>
                <Box sx={{marginTop: 9}}>
                    <Outlet/>
                </Box>
            </Container>
        </React.Fragment>
    )
}