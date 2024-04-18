import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function AppRolePage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("role")}
      </Typography>
      <Switch>
        <Route exact path="/approle" component={List} />
        <Route path="/approle/edit/:id?" component={Edit} />
      </Switch>
    </>
  );
}
