import React, {useEffect, useState} from "react";
import {Box, Card, CardContent, CircularProgress, Divider, Grid, Typography} from "@mui/material";
import Visit from "@models/Visit.ts";
import {AccessTime, Public} from "@mui/icons-material";
import VisitService from "@services/VisitService.ts";
import HttpClientVisit from "@api/HttpClientVisit.ts";

export default function ListVisitPage(): React.JSX.Element {
    const [visits, setVisits] = useState<Visit[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    useEffect(() => {
        setLoading(true);
        new VisitService(new HttpClientVisit()).getVisits().then(response => {
            setVisits(response.data);
            setLoading(false);
        }).catch(e => {
            console.log(e)
            setLoading(false);
        }).finally(() => {
            console.log();
        });
    }, [])

    return (
        <Box sx={{padding: 4, backgroundColor: '#f5f5f5'}}>
            <Grid container spacing={4} justifyContent="center">
                {loading ? (
                    <Box sx={{display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '200px'}}>
                        <CircularProgress/>
                    </Box>
                ) : (
                    visits.map((visit) => (
                        <Grid item xs={12} sm={6} md={4} key={visit.id}>
                            <Card sx={{
                                display: 'flex',
                                flexDirection: 'column',
                                borderRadius: 2,
                                boxShadow: 3,
                                backgroundColor: '#ffffff',
                                transition: 'transform 0.3s ease-in-out',
                                '&:hover': {
                                    transform: 'scale(1.05)',
                                },
                            }}>
                                <CardContent sx={{flexGrow: 1}}>
                                    <Typography variant="h6" color="primary" align="center">
                                        {visit.country} {visit.emoji && <span>{visit.emoji}</span>}
                                    </Typography>
                                    <Divider sx={{marginY: 2}}/>
                                    <Typography variant="body1" color="text.secondary" align="center">
                                        <Public sx={{verticalAlign: 'middle', marginRight: 1}}/>
                                        Latitude: {visit.latitude}, Longitude: {visit.longitude}
                                    </Typography>
                                    {visit.currency && (
                                        <Typography variant="body2" color="text.secondary" align="center"
                                                    sx={{marginTop: 1}}>
                                            Currency: <strong>{visit.currencyName} ({visit.symbol})</strong>
                                        </Typography>
                                    )}
                                    <Typography variant="body2" color="text.secondary" align="center"
                                                sx={{marginTop: 1}}>
                                        <AccessTime sx={{verticalAlign: 'middle', marginRight: 1}}/>
                                        Visit Date: {new Date(visit.visitDate).toLocaleDateString()}
                                    </Typography>
                                </CardContent>
                            </Card>
                        </Grid>
                    ))
                )}
            </Grid>
        </Box>
    );
}