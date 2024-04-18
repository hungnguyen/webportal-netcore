import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function PhrasePage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("phrase")}
      </Typography>
      <Switch>
        <Route exact path="/phrase" component={List} />
        <Route path="/phrase/edit/:id?" component={Edit} />
      </Switch>
    </>
  );
}
