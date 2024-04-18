import React from "react";
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from "chart.js";
import { Doughnut } from "react-chartjs-2";
import useStyles from "../../shared/styles";
import { Paper } from "@material-ui/core";

ChartJS.register(ArcElement, Tooltip, Legend);

export default function DoughnutChart({ rows, labels, title }) {
  const classes = useStyles();
  const options = {
    plugins: {
      legend: {
        position: "top",
      },
      title: {
        display: true,
        text: title,
        font: {
          size: 20,
        },
      },
    },
  };
  const data = {
    labels: labels,
    datasets: [
      {
        label: "# of Votes",
        data: rows,
        backgroundColor: [
          "rgba(255, 99, 132, 0.2)",
          "rgba(54, 162, 235, 0.2)",
          "rgba(255, 206, 86, 0.2)",
          "rgba(75, 192, 192, 0.2)",
          "rgba(153, 102, 255, 0.2)",
          "rgba(255, 159, 64, 0.2)",
        ],
        borderColor: [
          "rgba(255, 99, 132, 1)",
          "rgba(54, 162, 235, 1)",
          "rgba(255, 206, 86, 1)",
          "rgba(75, 192, 192, 1)",
          "rgba(153, 102, 255, 1)",
          "rgba(255, 159, 64, 1)",
        ],
        borderWidth: 1,
      },
    ],
  };
  return (
    <Paper className={classes.tablePaper + " " + classes.browserChart}>
      <Doughnut data={data} options={options} />
    </Paper>
  );
}
