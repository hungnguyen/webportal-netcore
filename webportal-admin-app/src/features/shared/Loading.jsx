import React from 'react';
import {Grid, CircularProgress} from "@material-ui/core"

export default function Loading() {
    return(
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="center"
          style={{ height: "calc(100vh-100px)" }}
        >
          <CircularProgress />
        </Grid>
    );
}