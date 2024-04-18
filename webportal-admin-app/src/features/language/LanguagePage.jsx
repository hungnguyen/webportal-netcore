import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function LanguagePage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("language")}
      </Typography>
      <Switch>
        <Route exact path="/language" component={List} />
        <Route path="/language/edit/:id?" component={Edit} />
      </Switch>
    </>
  );
}
