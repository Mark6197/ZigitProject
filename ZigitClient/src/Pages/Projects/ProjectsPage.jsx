import { Paper, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useAuthState } from "../../Context/Auth";
import "./ProjectsPage.css";
import axios from "axios";
import ProjectsTable from "../../Components/Table/ProjectsTable.jsx";

const ProjectsPage = () => {
  const [projects, setProjects] = useState([]);
  const { user, token } = useAuthState();
  useEffect(() => {
    fetchProjects();
  }, []);

  const fetchProjects = async () => {
    axios
      .get(`${process.env.REACT_APP_ROOT_URL}/projects`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((res) => {
        setProjects(
          res.data.map((p) => {
            let mappedProject = { ...p, id: p.projectGuid };
            delete mappedProject.projectGuid;

            return mappedProject;
          })
        );
      });
  };

  return (
    <>
      <Paper
        sx={{
          marginTop: "30px",
          width: "90%",
          padding: "20px",
          paddingTop: "10px",
        }}
      >
        <div className="detailsWrapper">
          <img src={user?.avatar} alt="Avatar" className="avatar" />
          <div className="detailsTextWrapper">
            <Typography variant="h6">User personal details:</Typography>
            <Typography variant="body2">Name: {user?.name}</Typography>
            <Typography variant="body2">Team: {user?.team}</Typography>
            <Typography variant="body2">
              Joined at: {new Date(user?.joinedAt).toLocaleDateString()}
            </Typography>
          </div>
        </div>
      </Paper>
      <ProjectsTable rows={projects} />
    </>
  );
};

export default ProjectsPage;
