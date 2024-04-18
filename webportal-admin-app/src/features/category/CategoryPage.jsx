import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Edit from "./Edit";
import List from "./List";
import { useTranslation } from "react-i18next";

export default function CategoryPage() {
  const { t } = useTranslation();
  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("category")}
      </Typography>
      <Switch>
        <Route exact path="/category" component={List} />
        <Route path="/category/edit/:id?" component={Edit} />
      </Switch>
    </>
  );
}
