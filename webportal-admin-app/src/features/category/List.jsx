import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { categorySelector } from "../../redux/category/categorySlice";
import { getPagingAsync } from "../../redux/category/categoryAsyncThunk";
import { productTypeSelector } from "../../redux/productType/productTypeSlice";
import useStyles from "../shared/styles";
import { NavLink } from "react-router-dom";
import { useTranslation } from "react-i18next";

import {
  Button,
  ListSubheader,
  List,
  CardContent,
  Card,
  CardHeader,
  IconButton,
  Divider,
  Tooltip,
} from "@material-ui/core";
import CatListView from "./CatListView";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { Refresh } from "@material-ui/icons";
import Loading from "../shared/Loading";

export default function ListCat() {
  const dispatch = useDispatch();
  const category = useSelector(categorySelector);
  const productType = useSelector(productTypeSelector);
  const application = useSelector(applicationSelector);
  const classes = useStyles();
  const { t } = useTranslation();

  const loadData = () => {
    dispatch(
      getPagingAsync({
        pagesize: 0,
        websiteid: application.website.id,
        languageid: application.languageid,
      })
    );
  };

  useEffect(() => {
    if (category.list.length === 0) {
      loadData();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const catNoType = category.list.filter((j) => j.typecode === "");
  return (
    <>
      <Button variant="outlined" color="primary">
        <NavLink to="/category/edit" className={classes.link}>
          {t("add-new")}
        </NavLink>
      </Button>
      <Card className={classes.tablePaper}>
        <CardHeader
          title={t("list")}
          action={
            <>
              <Tooltip title={t("refresh-lish")}>
                <IconButton aria-label="settings" onClick={loadData}>
                  <Refresh />
                </IconButton>
              </Tooltip>
            </>
          }
        />
        <Divider />
        <CardContent>
          {productType.loading && (<Loading />)}
          {productType.list.map((i) => {
            let catsByType = category.list.filter((j) => j.typecode === i.code);
            return (
              <React.Fragment key={i.id}>
                {catsByType.length > 0 && (
                  <List
                    component="nav"
                    subheader={
                      <ListSubheader component="div" id="nested-list-subheader">
                        {i.name}
                      </ListSubheader>
                    }
                  >
                    <CatListView all={catsByType} parentid={0} className="" />
                  </List>
                )}
              </React.Fragment>
            );
          })}

          {catNoType.length > 0 && (
              <List
                component="nav"
                subheader={
                  <ListSubheader component="div" id="nested-list-subheader">
                    No Type
                  </ListSubheader>
                }
              >
                <CatListView all={catNoType} parentid={0} className="" />
              </List>
            )
          }
        </CardContent>
      </Card>
    </>
  );
}
