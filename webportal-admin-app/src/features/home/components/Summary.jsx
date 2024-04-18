import React from "react";
import { Card, CardContent, Grid, Typography, Avatar } from "@material-ui/core";
import useStyles from "../../shared/styles";

export default function Summary({
  title,
  titleColor,
  avatarColor,
  number,
  icon,
}) {
  const classes = useStyles();
  return (
    <>
      <Card className={classes.tablePaper}>
        <CardContent>
          <Grid container>
            <Grid item md={10}>
              <Typography variant="h6" gutterBottom className={titleColor}>
                {title}
              </Typography>
              <Typography variant="h3">{number}</Typography>
            </Grid>
            <Grid item container md={2} justifyContent="flex-end">
              <Avatar className={avatarColor}>{icon}</Avatar>
            </Grid>
          </Grid>
        </CardContent>
      </Card>
    </>
  );
}
