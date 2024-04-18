import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function ProductTypePage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("manage-type")}
      </Typography>
      <Switch>
        <Route exact path="/product-type" component={List} />
        <Route path="/product-type/edit/:id?" component={Edit} />
      </Switch>
    </>
  );
}
