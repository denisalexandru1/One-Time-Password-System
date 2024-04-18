import React, {useEffect, useState} from 'react';
import './App.css';
import background from './images/background.jpg';
import {toast, ToastContainer} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
    const [otpModel, setOtpModel] = useState(null);

    const handleClick = () => {
        const timeBound = document.getElementById("timeBound").value;
        let url = 'https://localhost:7156/OTP/generate';
        if (timeBound) {
            url += `?timeBound=${timeBound}`;
        }
        const headers = {
            'Allow-Origin': '*',
            'Content-Type': 'application/json',
        };

        fetch(url, {
            method: 'GET',
            headers: headers
        })
            .then(response => response.json())
            .then(data => setOtpModel(data))
            .catch(error => console.error('Error:', error));
    };

    useEffect(() => {
        const interval = setInterval(async () => {
            if (await otpIsValid(otpModel)) {
                toast.success('Here is Your Valid OTP: ' + otpModel.otp, {
                    autoClose: 10000,
                    position: "top-center"
                });
            }
        }, 10000);

        return () => clearInterval(interval);
    }, [otpModel]);

    const otpIsValid = async (otpModel) => {
        if (!otpModel) {
            return false;
        }
        const url = "https://localhost:7156/OTP/validate";
        const headers = {
            'Allow-Origin': '*',
            'Content-Type': 'application/json',
        };

        const response = await fetch(url, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify({ otp: otpModel.otp })
        });

        return await response.json();
    };

    return (
        <>
            <ToastContainer/>
            <div style={{
                backgroundImage: `url(${background})`,
                backgroundSize: 'cover',
                backgroundPosition: 'center',
                minHeight: '100vh',
                display: "flex",
                justifyContent: "center",
                alignItems: "center"
            }}>
                <div style={{
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "center",
                    alignItems: "center",
                    backgroundColor: "rgba(0, 0, 0, 0.3)",
                    maxWidth: '600px',
                    padding: "20px",
                    borderRadius: "20px",
                    boxShadow: "0 0 10px 0 #000"
                }}>
                    <h1 style={{color: "white", textAlign: "center", fontSize: "30px", fontWeight: "bold"}}>
                        This is an One-Time Password Generator!
                    </h1>
                    <h3 style={{color: "white", textAlign: "center", fontSize: "15px"}}>
                        Please fill in the form below to decide for how long you want the OTP to be valid.
                    </h3>
                    <h3 style={{color: "white", textAlign: "center", fontSize: "15px"}}>
                        If you don't fill in the form, the default value is 30 minutes.
                    </h3>
                    <h3 style={{color: "white", textAlign: "center", fontSize: "15px"}}>
                        Proceed by clicking the "Generate OTP" button.
                    </h3>
                    <form style={{display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "center"}}>
                        <label style={{color: "white", fontSize: "20px", fontWeight: "bold"}}>
                            OTP Validity (in minutes):
                        </label>
                        <input
                            type="number"
                            id="timeBound"
                            name="timeBound"
                            style={{
                                width: "100%",
                                padding: "12px 20px",
                                margin: "8px 0",
                                display: "inline-block",
                                border: "1px solid #ccc",
                                borderRadius: "4px",
                                boxSizing: "border-box"}}
                        />
                        <button type="button"
                                style={{
                                    width: "100%",
                                    backgroundColor: "#4CAF50",
                                    color: "white",
                                    padding: "14px 20px",
                                    margin: "8px 0",
                                    border: "none",
                                    borderRadius: "4px",
                                    cursor: "pointer"}}
                                onClick={handleClick}
                        >
                            Generate OTP
                        </button>
                    </form>
                </div>
            </div>
        </>
    );
}

export default App;
