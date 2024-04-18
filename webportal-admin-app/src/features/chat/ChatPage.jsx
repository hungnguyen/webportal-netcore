import React from "react";
import { Grid, Typography, Card, CardContent } from "@material-ui/core";
import { useTranslation } from "react-i18next";
import UserList from "./UserList";
import ChatBox from "./ChatBox";
import useStyles from "../shared/styles";

export default function ChatPage() {
  const { t } = useTranslation();
  const classes = useStyles();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("chatBox")}
      </Typography>
      <Card className={classes.tablePaper}>
        <CardContent>
          <Grid container spacing={2}>
            <Grid item xs={2}>
              <UserList />
            </Grid>
            <Grid item xs={10}>
              <ChatBox />
            </Grid>
          </Grid>
        </CardContent>
      </Card>
    </>
  );
}
