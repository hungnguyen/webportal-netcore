import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import { useTranslation } from "react-i18next";

export default function WebsitePage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("configuration")}
      </Typography>
      <Switch>
        <Route exact path="/configuration" component={Edit} />
      </Switch>
    </>
  );
}
