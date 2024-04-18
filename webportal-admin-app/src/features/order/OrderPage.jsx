import React from "react";
import { Switch, Route } from "react-router-dom";
import { Typography } from "@material-ui/core";
import Detail from "./Detail";
import List from "./List";

export default function OrderPage() {
  return (
    <>
      <Typography variant="h4" gutterBottom>
        Order
      </Typography>
      <Switch>
        <Route exact path="/order" component={List} />
        <Route path="/order/detail/:id?" component={Detail} />
      </Switch>
    </>
  );
}
