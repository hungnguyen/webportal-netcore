import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import {
  Grid,
  Divider,
  Typography,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
} from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initOrder,
  unselect,
  orderSelector,
} from "../../redux/order/orderSlice";
import { enumSelector } from "../../redux/enum/enumSlice";
import {
  getDetailByIdAsync,
  updateAsync,
} from "../../redux/order/orderAsyncThunk";
import { appuserSelector } from "../../redux/appuser/appuserSlice";
import { getAllAsync } from "../../redux/appuser/appuserAsyncThunk";
import { useSelector, useDispatch } from "react-redux";
import NumberFormat from "react-number-format";
import { getEnumKey } from "../shared/utils";
import { useTranslation } from "react-i18next";
import EditFormContainer from "../shared/EditFormContainer";

export default function Detail() {
  const history = useHistory();
  const classes = useStyles();
  const { id } = useParams();
  const [item, setItem] = useState(initOrder);
  const order = useSelector(orderSelector);
  const dispatch = useDispatch();
  const enums = useSelector(enumSelector);
  const appuser = useSelector(appuserSelector);
  const { t } = useTranslation();

  useEffect(() => {
    if (appuser.list.length === 0) {
      dispatch(getAllAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (id) {
      if (order.item.id !== id) {
        dispatch(getDetailByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setItem(order.item);
  }, [order.item]);

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/order");
  };

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleSave = () => {
    dispatch(updateAsync(item));
    dispatch(unselect());
    history.push("/order");
  };

  return (
    <>
      <EditFormContainer
        handleCancel={handleCancel}
        handleSave={handleSave}
        loading={order.loading}
        title={t("detail")}
      >
        <form autoComplete="off" className={classes.form}>
          <Grid container spacing={3}>
            <Grid item md={4}>
              <FormControl className={classes.formControl} variant="outlined">
                <InputLabel id="saleid-label">{t("saler")}</InputLabel>
                <Select
                  labelId="saleid-label"
                  id="saleid"
                  name="saleid"
                  value={item.saleid}
                  onChange={handleChange}
                  label={t("saler")}
                >
                  <MenuItem key={0} value={0}>
                    <em>{t("none")}</em>
                  </MenuItem>
                  {appuser.list.map((i) => (
                    <MenuItem key={i.id} value={i.id}>
                      {i.username}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item md={4}>
              <FormControl className={classes.formControl} variant="outlined">
                <InputLabel id="orderstatus-label">
                  {t("order-status")}
                </InputLabel>
                <Select
                  labelId="orderstatus-label"
                  id="orderstatus"
                  name="orderstatus"
                  value={item.orderstatus}
                  onChange={handleChange}
                  label={t("order-status")}
                >
                  <MenuItem key={0} value={0}>
                    <em>{t("none")}</em>
                  </MenuItem>
                  {enums.orderstatus.map((i) => (
                    <MenuItem key={i.value} value={i.value}>
                      {t(i.key)}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item md={4}>
              <FormControl className={classes.formControl} variant="outlined">
                <InputLabel id="paystatus-label">
                  {t("payment-status")}
                </InputLabel>
                <Select
                  labelId="paystatus-label"
                  id="paystatus"
                  name="paystatus"
                  value={item.paystatus}
                  onChange={handleChange}
                  label={t("payment-status")}
                >
                  <MenuItem key={0} value={0}>
                    <em>{t("none")}</em>
                  </MenuItem>
                  {enums.paystatus.map((i) => (
                    <MenuItem key={i.value} value={i.value}>
                      {t(i.key)}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item md={12}>
              <Divider />
            </Grid>
            <Grid item container md={12}>
              <Grid item md={6}>
                <Typography variant="h5">{t("customer-info")}</Typography>
              </Grid>
              <Grid item md={6}>
                <Typography variant="h5">{t("shipping-info")}</Typography>
              </Grid>
            </Grid>
            <Grid item container md={12}>
              <Grid item md={2}>
                <Typography variant="subtitle2">{t("full-name")}:</Typography>
              </Grid>
              <Grid item md={4}>
                <Typography>{item.customer.fullname}</Typography>
              </Grid>
              <Grid item md={2}>
                <Typography variant="subtitle2">{t("full-name")}:</Typography>
              </Grid>
              <Grid item md={4}>
                <Typography>{item.shippingname}</Typography>
              </Grid>
            </Grid>
            <Grid item container md={12}>
              <Grid item md={2}>
                <Typography variant="subtitle2">{t("email")}:</Typography>
              </Grid>
              <Grid item md={4}>
                <Typography>{item.customer.email}</Typography>
              </Grid>
              <Grid item md={2}>
                <Typography variant="subtitle2">{t("email")}:</Typography>
              </Grid>
              <Grid item md={4}>
                <Typography>{item.shippingemail}</Typography>
              </Grid>
            </Grid>
            <Grid item container md={12}>
              <Grid item md={2}>
                <Typography variant="subtitle2">
                  {t("phone-number")}:
                </Typography>
              </Grid>
              <Grid item md={4}>
                <Typography>{item.customer.phonenumber}</Typography>
              </Grid>
              <Grid item md={2}>
                <Typography variant="subtitle2">
                  {t("phone-number")}:
                </Typography>
              </Grid>
              <Grid item md={4}>
                <Typography>{item.shippingphone}</Typography>
              </Grid>
            </Grid>
            <Grid item container md={12}>
              <Grid item md={2}>
                <Typography variant="subtitle2">{t("address")}:</Typography>
              </Grid>
              <Grid item md={4}>
                <Typography>{item.customer.address}</Typography>
              </Grid>
              <Grid item md={2}>
                <Typography variant="subtitle2">{t("address")}:</Typography>
              </Grid>
              <Grid item md={4}>
                <Typography>{item.shippingaddress}</Typography>
              </Grid>
            </Grid>
            <Grid item container md={12}>
              <Typography variant="h5">{t("payment-method")}</Typography>
            </Grid>
            <Grid item container md={12}>
              <Typography variant="subtitle2">
                {t(getEnumKey(enums.paymethod, item.paymethod))}
              </Typography>
            </Grid>
            <Grid item container md={12}>
              <Typography variant="h5">{t("item-list")}</Typography>
            </Grid>
          </Grid>
          <TableContainer>
            <Table className={classes.table} aria-label="spanning table">
              <TableHead>
                <TableRow>
                  <TableCell>{t("name")}</TableCell>
                  <TableCell align="right">{t("price")}</TableCell>
                  <TableCell align="right">{t("quantity")}</TableCell>
                  <TableCell align="right">{t("amount")}</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {item.orderitems.map((row) => (
                  <TableRow key={row.id}>
                    <TableCell>{row.productid}</TableCell>
                    <TableCell align="right">{row.price}</TableCell>
                    <TableCell align="right">{row.quantity}</TableCell>
                    <TableCell align="right">
                      <NumberFormat
                        displayType="text"
                        value={row.amount}
                        thousandSeparator
                      />
                    </TableCell>
                  </TableRow>
                ))}

                <TableRow>
                  <TableCell rowSpan={3} colSpan={2} />
                  <TableCell align="right">{t("total")}</TableCell>
                  <TableCell align="right">
                    <NumberFormat
                      displayType="text"
                      value={item.totalamout}
                      thousandSeparator
                    />
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </TableContainer>
        </form>
      </EditFormContainer>
    </>
  );
}
