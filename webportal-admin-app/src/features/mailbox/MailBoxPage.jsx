import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Detail from "./Detail";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function MailBoxPage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("mail-box")}
      </Typography>
      <Switch>
        <Route exact path="/mailbox" component={List} />
        <Route path="/mailbox/detail/:id?" component={Detail} />
      </Switch>
    </>
  );
}
