import { Grid } from "@material-ui/core";
import { Alert, AlertTitle } from "@material-ui/lab";
import React from "react";

export default function ValidatorSummary({ errors }) {
  return (
    <>
      {errors.length > 0 && (
        <Grid item md={12}>
          <Alert severity="error">
            <AlertTitle>Error</AlertTitle>
            <ul>
              {errors.map((e, i) => (
                <li key={i}>{e}</li>
              ))}
            </ul>
          </Alert>
        </Grid>
      )}
    </>
  );
}
