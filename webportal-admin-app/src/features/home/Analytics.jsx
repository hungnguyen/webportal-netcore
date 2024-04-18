import React, { useEffect } from "react";
import { Typography, Grid, CircularProgress } from "@material-ui/core";
import { useTranslation } from "react-i18next";
import useStyles from "../shared/styles";
import Summary from "./components/Summary";
import MultiLineChart from "./components/MultiLineChart";
import {
  analyticsSelector,
  setLoaded,
} from "../../redux/analytics/analyticsSlice";
import { useDispatch, useSelector } from "react-redux";
import DoughnutChart from "./components/DoughnutChart";
import TopList from "./components/TopList";
import {
  getGraphAsync,
  getSummaryAsync,
  getTopListAsync,
} from "../../redux/analytics/analyticsAsyncThunk";
import { Pageview, ShoppingCart, Person, AccessTime } from "@material-ui/icons";

import { useState } from "react";
import moment from "moment";

export default function Analytics() {
  const { t } = useTranslation();
  const classes = useStyles();
  const analytics = useSelector(analyticsSelector);
  const dispatch = useDispatch();
  const [filter] = useState({
    startDate: moment().add(-30, "days").format("yyyy-MM-DD"),
    endDate: moment().format("yyyy-MM-DD"),
    propertyId: "373328729",
  });

  const loadData = () => {
    dispatch(
      getSummaryAsync({
        ...filter,
        metrics: [
          "screenPageViews",
          "activeUsers",
          "ecommercePurchases",
          "userEngagementDuration",
        ],
      })
    );

    //top list
    dispatch(
      getTopListAsync({
        type: "pageviews",
        request: {
          ...filter,
          metrics: ["screenPageViews"],
          dimensions: ["pageTitle"],
        },
      })
    );
    dispatch(
      getTopListAsync({
        type: "items",
        request: {
          ...filter,
          metrics: ["itemsPurchased"],
          dimensions: ["itemName"],
        },
      })
    );
    dispatch(
      getTopListAsync({
        type: "cities",
        request: {
          ...filter,
          metrics: ["sessions"],
          dimensions: ["city"],
        },
      })
    );
    dispatch(
      getTopListAsync({
        type: "browsers",
        request: {
          ...filter,
          metrics: ["sessions"],
          dimensions: ["browser"],
        },
      })
    );
    dispatch(
      getTopListAsync({
        type: "events",
        request: {
          ...filter,
          metrics: ["eventCount"],
          dimensions: ["eventName"],
        },
      })
    );

    //graph
    dispatch(
      getGraphAsync({
        type: "pageviews",
        request: {
          ...filter,
          metrics: ["screenPageViews"],
          dimensions: ["date"],
        },
      })
    );
    dispatch(
      getGraphAsync({
        type: "activeusers",
        request: {
          ...filter,
          metrics: ["activeUsers"],
          dimensions: ["date"],
        },
      })
    );
    dispatch(
      getGraphAsync({
        type: "itempurchased",
        request: {
          ...filter,
          metrics: ["itemsPurchased"],
          dimensions: ["date"],
        },
      })
    );

    //mark loaded
    dispatch(setLoaded());
  };

  const convertNumberToTime = (number) => {
    var hours = Math.floor(number / 60) ?? 0;
    var minutes = number % 60 ?? 0;
    return `${isNaN(hours)?0:hours}m${isNaN(minutes)?0:minutes}s`;
  };

  useEffect(() => {
    loadData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("analytics")}
      </Typography>
      {analytics.loading ? (
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="center"
          style={{ height: "calc(100vh-100px)" }}
        >
          <CircularProgress />
        </Grid>
      ) : (
        <Grid container spacing={3}>
          {/* Summary */}
          {Object.keys(analytics.summary).length > 0 && (
            <>
              <Grid item md={3}>
                <Summary
                  title={t("total-page-views")}
                  titleColor={classes.colorBlue}
                  avatarColor={classes.avatarBlue}
                  number={analytics.summary.rows[0]?.metricvalues[0].value ?? 0}
                  icon={<Pageview />}
                />
              </Grid>
              <Grid item md={3}>
                <Summary
                  title={t("total-customer")}
                  titleColor={classes.colorGreen}
                  avatarColor={classes.avatarGreen}
                  number={analytics.summary.rows[0]?.metricvalues[1].value ?? 0}
                  icon={<Person />}
                />
              </Grid>
              <Grid item md={3}>
                <Summary
                  title={t("total-booking")}
                  titleColor={classes.colorRed}
                  avatarColor={classes.avatarRed}
                  number={analytics.summary.rows[0]?.metricvalues[2].value ?? 0}
                  icon={<ShoppingCart />}
                />
              </Grid>
              <Grid item md={3}>
                <Summary
                  title={t("average-engagement-time")}
                  titleColor={classes.colorBlue}
                  avatarColor={classes.avatarBlue}
                  number={convertNumberToTime(
                    Math.round(
                      analytics.summary.rows[0]?.metricvalues[3].value ?? 0 /
                        analytics.summary.rows[0]?.metricvalues[1].value ?? 1
                    )
                  )}
                  icon={<AccessTime />}
                />
              </Grid>
            </>
          )}

          {/* Graph */}
          <Grid item md={9}>
            {Object.keys(analytics.graph.pageviews).length > 0 &&
              Object.keys(analytics.graph.activeusers).length > 0 &&
              Object.keys(analytics.graph.itempurchased).length > 0 && (
                <MultiLineChart
                  title={t("sale-chart")}
                  datasets={[
                    {
                      label: t("page-views"),
                      data: analytics.graph.pageviews.values,
                      borderColor: "rgb(255, 99, 132)",
                      backgroundColor: "rgba(255, 99, 132, 0.5)",
                    },
                    {
                      label: t("activeUsers"),
                      data: analytics.graph.activeusers.values,
                      borderColor: "rgb(53, 162, 235)",
                      backgroundColor: "rgba(53, 162, 235, 0.5)",
                    },
                    {
                      label: t("item-purchased"),
                      data: analytics.graph.itempurchased.values,
                      borderColor: "rgb(75, 192, 192)",
                      backgroundColor: "rgba(75, 192, 192, 0.2)",
                    },
                  ]}
                  labels={analytics.graph.pageviews.labels}
                />
              )}
          </Grid>
          <Grid item md={3}>
            {Object.keys(analytics.toplist.browsers).length > 0 && (
              <DoughnutChart
                rows={analytics.toplist.browsers.rows?.map(
                  (r) => r.metricvalues[0].value
                )}
                labels={analytics.toplist.browsers.rows?.map(
                  (r) => r.dimensionvalues[0].value
                )}
                title={t("browsers-chart")}
              />
            )}
          </Grid>

          {/* Top list */}
          <Grid item md={3}>
            {Object.keys(analytics.toplist.pageviews).length > 0 && (
              <TopList
                title={t("top-page-views")}
                rows={analytics.toplist.pageviews.rows.map((r) => ({
                  name: r.dimensionvalues[0].value,
                  value: r.metricvalues[0].value,
                }))}
              />
            )}
          </Grid>
          <Grid item md={3}>
            {Object.keys(analytics.toplist.items).length > 0 && (
              <TopList
                title={t("top-products-sold")}
                rows={analytics.toplist.items.rows.map((r) => ({
                  name: r.dimensionvalues[0].value,
                  value: r.metricvalues[0].value,
                }))}
              />
            )}
          </Grid>
          <Grid item md={3}>
            {Object.keys(analytics.toplist.cities).length > 0 && (
              <TopList
                title={t("top-cities-visit")}
                rows={analytics.toplist.cities.rows.map((r) => ({
                  name: r.dimensionvalues[0].value,
                  value: r.metricvalues[0].value,
                }))}
              />
            )}
          </Grid>
          <Grid item md={3}>
            {Object.keys(analytics.toplist.events).length > 0 && (
              <TopList
                title={t("top-events")}
                rows={analytics.toplist.events.rows.map((r) => ({
                  name: r.dimensionvalues[0].value,
                  value: r.metricvalues[0].value,
                }))}
              />
            )}
          </Grid>
        </Grid>
      )}
    </>
  );
}
