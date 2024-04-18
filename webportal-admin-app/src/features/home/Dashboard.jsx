import {
  Avatar,
  Card,
  CardContent,
  Grid,
  IconButton,
  Paper,
  Tooltip,
  Typography,
} from "@material-ui/core";
import {
  Edit,
  Inbox,
  People,
  ShoppingCart,
  Subtitles,
  Visibility,
} from "@material-ui/icons";
import React, { useEffect, useState } from "react";
import useStyles from "../shared/styles";
import TableView from "../shared/TableView";
import { useSelector, useDispatch } from "react-redux";
import { productSelector } from "../../redux/product/productSlice";
import {
  getPagingAsync as getProduct,
  getCountAsync as getCountProduct,
} from "../../redux/product/productAsyncThunk";
import { orderSelector } from "../../redux/order/orderSlice";
import {
  getPagingAsync as getOrder,
  getCountAsync as getCountOrder,
} from "../../redux/order/orderAsyncThunk";
import { customerSelector } from "../../redux/customer/customerSlice";
import { getCountAsync as getCountCustomer } from "../../redux/customer/customerAsyncThunk";
import Moment from "react-moment";
import NumberFormat from "react-number-format";
import { enumSelector } from "../../redux/enum/enumSlice";
import { getEnumKey } from "../shared/utils";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { NavLink } from "react-router-dom";
import { useTranslation } from "react-i18next";

const Dashboard = () => {
  const classes = useStyles();
  const dispatch = useDispatch();
  const product = useSelector(productSelector);
  const order = useSelector(orderSelector);
  const customer = useSelector(customerSelector);
  const enums = useSelector(enumSelector);

  const application = useSelector(applicationSelector);
  const [latestProduct, setLatestProduct] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (product.list.length === 0) {
      dispatch(
        getProduct({
          pagesize: 0,
          websiteid: application.website.id,
          languageid: application.languageid,
        })
      );
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setLatestProduct(
      product.list.filter((i) => i.typecode === "PRD").slice(0, 10)
    );
  }, [product.list]);

  useEffect(() => {
    if (order.list.length === 0) {
      dispatch(
        getOrder({
          pagesize: 10,
          pageindex: 1,
          websiteid: application.website.id,
        })
      );
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (customer.total === 0) {
      dispatch(
        getCountCustomer({ pagesize: 0, websiteid: application.website.id })
      );
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (order.total === 0) {
      dispatch(
        getCountOrder({ pagesize: 0, websiteid: application.website.id })
      );
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (product.totalProduct === 0) {
      dispatch(
        getCountProduct({
          pagesize: 0,
          typecode: "PRD",
          websiteid: application.website.id,
          languageid: application.languageid,
        })
      );
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (product.totalNews === 0) {
      dispatch(
        getCountProduct({
          pagesize: 0,
          typecode: "NWS",
          websiteid: application.website.id,
          languageid: application.languageid,
        })
      );
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [product.totalProduct]);

  const productColumns = [
    { field: "name", headerName: t("name"), flex: 1 },
    {
      field: "dateupdated",
      headerName: t("date-updated"),
      flex: 0.5,
      type: "date",
      renderCell: (params) => (
        <Moment format="DD/MM/YYYY">{params.value}</Moment>
      ),
    },
    { field: "updatedby", headerName: t("update-by"), flex: 0.5 },
    {
      field: "dummy",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/product/${params.row.typecode}/edit/${params.id}`}>
            <Tooltip title={t("edit")}>
              <IconButton color="primary" size="small">
                <Edit />
              </IconButton>
            </Tooltip>
          </NavLink>
        </strong>
      ),
    },
  ];

  const orderColumns = [
    { field: "id", headerName: "ID", flex: 0.5 },
    { field: "customername", headerName: t("customer"), flex: 1 },
    {
      field: "orderstatus",
      headerName: t("status"),
      flex: 1,
      renderCell: (params) => (
        <>{t(getEnumKey(enums.orderstatus, params.row.orderstatus))}</>
      ),
    },
    {
      field: "totalamout",
      headerName: t("total-amount"),
      flex: 1,
      renderCell: (params) => (
        <NumberFormat
          displayType="text"
          value={params.value}
          thousandSeparator
        />
      ),
    },
    {
      field: "dummy",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/order/detail/${params.row.id}`}>
            <Tooltip title={t("view")}>
              <IconButton color="primary" size="small">
                <Visibility />
              </IconButton>
            </Tooltip>
          </NavLink>
        </strong>
      ),
    },
  ];

  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("dashboard")}
      </Typography>
      <Grid container spacing={3}>
        <Grid item md={3}>
          <Card className={classes.tablePaper}>
            <CardContent>
              <Grid container>
                <Grid item md={10}>
                  <Typography
                    variant="h6"
                    gutterBottom
                    className={classes.colorBlue}
                  >
                    {t("total-product")}
                  </Typography>
                  <Typography variant="h3">{product.totalProduct}</Typography>
                </Grid>
                <Grid item container md={2} justifyContent="flex-end">
                  <Avatar className={classes.avatarBlue}>
                    <Inbox />
                  </Avatar>
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Grid>
        <Grid item md={3}>
          <Card className={classes.tablePaper}>
            <CardContent>
              <Grid container>
                <Grid item md={10}>
                  <Typography
                    variant="h6"
                    gutterBottom
                    className={classes.colorRed}
                  >
                    {t("total-booking")}
                  </Typography>
                  <Typography variant="h3">{order.total}</Typography>
                </Grid>
                <Grid item container md={2} justifyContent="flex-end">
                  <Avatar className={classes.avatarRed}>
                    <ShoppingCart />
                  </Avatar>
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Grid>
        <Grid item md={3}>
          <Card className={classes.tablePaper}>
            <CardContent>
              <Grid container>
                <Grid item md={10}>
                  <Typography
                    variant="h6"
                    gutterBottom
                    className={classes.colorGreen}
                  >
                    {t("total-customer")}
                  </Typography>
                  <Typography variant="h3">{customer.total}</Typography>
                </Grid>
                <Grid item container md={2} justifyContent="flex-end">
                  <Avatar className={classes.avatarGreen}>
                    <People />
                  </Avatar>
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Grid>
        <Grid item md={3}>
          <Card className={classes.tablePaper}>
            <CardContent>
              <Grid container>
                <Grid item md={10}>
                  <Typography
                    variant="h6"
                    gutterBottom
                    className={classes.colorOrange}
                  >
                    {t("total-news")}
                  </Typography>
                  <Typography variant="h3">{product.totalNews}</Typography>
                </Grid>
                <Grid item container md={2} justifyContent="flex-end">
                  <Avatar className={classes.avatarOrange}>
                    <Subtitles />
                  </Avatar>
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Grid>
        <Grid item md={6}>
          <Paper className={classes.tablePaper}>
            <TableView
              rows={order.list}
              loading={order.loading}
              columns={orderColumns}
              searchColumn={["name", "id"]}
              onRefresh={() =>
                dispatch(getOrder({ pagesize: 10, pageindex: 1 }))
              }
              title={t("latest-order")}
              hideFooter={true}
              height={460}
              rowHeight={40}
            />
          </Paper>
        </Grid>
        <Grid item md={6}>
          <Paper className={classes.tablePaper}>
            <TableView
              rows={latestProduct}
              loading={product.loading}
              columns={productColumns}
              searchColumn={["name", "id"]}
              onRefresh={() =>
                dispatch(getProduct({ pagesize: 10, pageindex: 1 }))
              }
              title={t("latest-product")}
              hideFooter={true}
              height={460}
              rowHeight={40}
            />
          </Paper>
        </Grid>
      </Grid>
    </>
  );
};

export default Dashboard;
