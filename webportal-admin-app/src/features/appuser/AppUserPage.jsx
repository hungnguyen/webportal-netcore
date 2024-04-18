import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function AppUserPage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("user")}
      </Typography>
      <Switch>
        <Route exact path="/appuser" component={List} />
        <Route path="/appuser/edit/:id?" component={Edit} />
      </Switch>
    </>
  );
}
