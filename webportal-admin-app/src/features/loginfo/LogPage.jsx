import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function LogPage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("log")}
      </Typography>
      <Switch>
        <Route exact path="/loginfo" component={List} />
      </Switch>
    </>
  );
}
