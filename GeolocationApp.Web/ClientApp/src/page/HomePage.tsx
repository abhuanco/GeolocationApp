import React, { useEffect, useState } from "react";
import VisitService from "@services/VisitService.ts";
import HttpClientVisit from "@api/HttpClientVisit.ts";
import Visit from "@models/Visit.ts";
import { Alert, Box, Card, CardContent, CardMedia, CircularProgress, Grid } from "@mui/material";
import Typography from "@mui/material/Typography";

export default function HomePage(): React.JSX.Element {
    const [visit, setVisit] = useState<Visit | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        let isMounted = true;

        const fetchData = async () => {
            const service = new VisitService(new HttpClientVisit());
            setLoading(true);
            try {
                const response = await service.tracking();
                if (isMounted) {
                    setVisit(response);
                }
            } catch (error) {
                console.log(error);
            } finally {
                if (isMounted) {
                    setLoading(false);
                }
            }
        };

        if (!visit) {
            fetchData();
        }

        return () => {
            isMounted = false;
        };
    }, []);

    return (
        <Box
            sx={{
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                minHeight: "70vh",
                backgroundColor: "#f4f6f8",
            }}
        >
            <Grid container spacing={2} justifyContent="center">
                <Grid item xs={12} sm={8} md={6}>
                    <Card
                        sx={{
                            display: "flex",
                            flexDirection: "column",
                            borderRadius: 2,
                            boxShadow: 3,
                            paddingTop: 2,
                            backgroundColor: "#ffffff",
                        }}
                    >
                        {loading ? (
                            <Box
                                sx={{
                                    display: "flex",
                                    justifyContent: "center",
                                    alignItems: "center",
                                    minHeight: "200px",
                                }}
                            >
                                <CircularProgress />
                            </Box>
                        ) : visit ? (
                            <>
                                <CardMedia
                                    component="img"
                                    height="200"
                                    width="200"
                                    image="/tracking-data.png"
                                    alt="Tracking data"
                                    sx={{ objectFit: "contain" }}
                                />
                                <CardContent>
                                    <Typography variant="h4" gutterBottom align="center">
                                        Welcome to our site!
                                    </Typography>
                                    <Typography variant="h6" color="text.secondary" align="center">
                                        We detected that you are in <strong>{visit.country} {visit.emoji}</strong>.
                                    </Typography>
                                    <Typography variant="h6" color="text.secondary" align="center">
                                        {visit.symbol ? (
                                            <p>
                                                The currency in your country is {visit.currencyName} ({visit.symbol}).
                                            </p>
                                        ) : (
                                            <Alert severity="info">
                                                We couldn't detect your currency, sorry.
                                            </Alert>
                                        )}
                                    </Typography>
                                </CardContent>
                            </>
                        ) : (
                            <Typography variant="h6" color="text.secondary" align="center">
                                We couldn't determine your location at the moment.
                            </Typography>
                        )}
                    </Card>
                </Grid>
            </Grid>
        </Box>
    );
}
