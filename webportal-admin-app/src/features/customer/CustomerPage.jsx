import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function CustomerPage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("customer")}
      </Typography>
      <Switch>
        <Route exact path="/customer" component={List} />
        <Route path="/customer/edit/:id?" component={Edit} />
      </Switch>
    </>
  );
}
