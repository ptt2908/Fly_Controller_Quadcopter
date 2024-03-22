/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * @attention
  *
  * Copyright (c) 2024 STMicroelectronics.
  * All rights reserved.
  *
  * This software is licensed under terms that can be found in the LICENSE file
  * in the root directory of this software component.
  * If no LICENSE file comes with this software, it is provided AS-IS.
  *
  ******************************************************************************
  */
/* USER CODE END Header */
/* Includes ------------------------------------------------------------------*/
#include "main.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include "stdbool.h"
#include "math.h"
#include "stdlib.h"
#include "stdio.h"
#include "string.h"
#include "BMP280_STM32.h"
#include "wire.h"

#define pi 3.14159
#define MPU6050_ADDR 0xD0
#define SMPLRT_DIV_REG 0x19
#define GYRO_CONFIG_REG 0x1B
#define ACCEL_CONFIG_REG 0x1C
#define ACCEL_XOUT_H_REG 0x3B
#define TEMP_OUT_H_REG 0x41
#define GYRO_XOUT_H_REG 0x43
#define PWR_MGMT_1_REG 0x6B
#define WHO_AM_I_REG 0x75
#define CONFIG_REG 0x1A
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */
uint8_t  buffer[5];  //twi buffer, for now 32 elements, for other purpose (e.g. for OLED it is 1024) change as you wish
uint8_t  address=0x77, length=0; //0xEC
uint16_t calibrationData[7];
int64_t OFF, OFF2, SENS, SENS2;
int pressure, D1, D2, dT, P, Pa, TEMP, T2;
float Altitude;
uint8_t Flag_Plot = 0;
uint8_t Rx_Data[63];
uint8_t Rx_buff[63];
uint8_t Tx_buff[70];
uint16_t size_RxData = 0;

uint8_t Tx_data[70];

volatile long ch[8];
volatile long tick;
volatile uint8_t pulse;
volatile float AngleRoll;
volatile float AnglePitch;

int16_t Accel_X_RAW = 0;
int16_t Accel_Y_RAW = 0;
int16_t Accel_Z_RAW = 0;

int16_t Gyro_X_RAW = 0;
int16_t Gyro_Y_RAW = 0;
int16_t Gyro_Z_RAW = 0;

float Ax, Ay, Az, Gx, Gy, Gz;
float AccZInertial;
float VelocityVertical;
long Gyro_X_Calib,Gyro_Y_Calib,Gyro_Z_Calib;
double Altitude_Calib;

int loop_timer;

int receiver_input_channel_1;
int receiver_input_channel_2;
int receiver_input_channel_3;
int receiver_input_channel_4;
int receiver_input_channel_5;
int receiver_input_channel_6;
/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
float KalmanAngleRoll=0, KalmanUncertaintyAngleRoll=2*2;
float KalmanAnglePitch=0, KalmanUncertaintyAnglePitch=2*2;
float Kalman1DOutput[] = {0,0};
//float AltitudeKalman = 0,VelocityVerticalKalman = 0;
//float F[2][2]; float G[2][1];float  P[2][2];float  Q[2][2];float S[2][1];
//float H[1][2]; float I[2][2];float Acc[1][1];float K[2][1];float R[1][1];float L[1][1];float M[1][1];
float DesiredRateRoll, DesiredRatePitch,DesiredRateYaw;
float ErrorRateRoll, ErrorRatePitch, ErrorRateYaw;
float InputRoll, InputThrottle, InputPitch, InputYaw;
float PrevErrorRateRoll, PrevErrorRatePitch, PrevErrorRateYaw;
float PrevItermRateRoll, PrevItermRatePitch, PrevItermRateYaw;

float PIDReturn[]={0, 0, 0};
float PRateRoll=0; float PRatePitch=0; float PRateYaw=0;
float IRateRoll=0; float IRatePitch=0; float IRateYaw=0;
float DRateRoll=0; float DRatePitch=0; float DRateYaw=0;
float MotorInput1, MotorInput2, MotorInput3, MotorInput4;

float DesiredAngleRoll, DesiredAnglePitch;
float ErrorAngleRoll, ErrorAnglePitch;
float PrevErrorAngleRoll, PrevErrorAnglePitch;
float PrevItermAngleRoll, PrevItermAnglePitch;
float PAngleRoll=0; float PAnglePitch=0;
float IAngleRoll=0; float IAnglePitch=0;
float DAngleRoll=0; float DAnglePitch=0;
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */
int SetValue(int value, int inMin, int inMax, int outMin, int outMax);
/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
I2C_HandleTypeDef hi2c1;
I2C_HandleTypeDef hi2c2;
DMA_HandleTypeDef hdma_i2c1_rx;
DMA_HandleTypeDef hdma_i2c1_tx;

SPI_HandleTypeDef hspi3;

TIM_HandleTypeDef htim1;
TIM_HandleTypeDef htim2;
TIM_HandleTypeDef htim3;
TIM_HandleTypeDef htim4;

UART_HandleTypeDef huart2;
UART_HandleTypeDef huart3;
DMA_HandleTypeDef hdma_usart2_rx;
DMA_HandleTypeDef hdma_usart2_tx;
DMA_HandleTypeDef hdma_usart3_rx;
DMA_HandleTypeDef hdma_usart3_tx;

/* USER CODE BEGIN PV */
void fault_dec()
{
	while(1);
}
void Send_Data_To_Gui()
{
	 Tx_buff[0] = 0x44;
   Tx_buff[1] = 0x41;
	 unsigned char *AngleRoll_Bytes = (unsigned char *)&KalmanAngleRoll;
    for (int i = 0; i < sizeof(float); ++i) {
        Tx_buff[i + 2] = AngleRoll_Bytes[i];
    }
		unsigned char *AnglePitch_Bytes = (unsigned char *)&KalmanAnglePitch;
    for (int i = 0; i < sizeof(float); ++i) {
        Tx_buff[i + 6] = AnglePitch_Bytes[i];
    }
		unsigned char *AngleRoll_Setpoint_Bytes = (unsigned char *)&DesiredAngleRoll;
    for (int i = 0; i < sizeof(float); ++i) {
        Tx_buff[i + 10] = AngleRoll_Setpoint_Bytes[i];
    }
		unsigned char *AnglePitch_Setpoint_Bytes = (unsigned char *)&DesiredAnglePitch;
    for (int i = 0; i < sizeof(float); ++i) {
        Tx_buff[i + 14] = AnglePitch_Setpoint_Bytes[i];
    }
		
		Tx_buff[18] = '\r'; Tx_buff[19] = '\n';
	if(Flag_Plot)
	{
		HAL_UART_Transmit_DMA(&huart2, Tx_buff,20);
	}
}

void kalman_1d(float KalmanState, float KalmanUncertainty, float KalmanInput, float KalmanMeasurement) {
  KalmanState = KalmanState + 0.004*KalmanInput;
  KalmanUncertainty = KalmanUncertainty + 0.004 * 0.004 * 4 * 4;
  float KalmanGain = KalmanUncertainty * 1/(1*KalmanUncertainty + 3 * 3);
  KalmanState = KalmanState+KalmanGain * (KalmanMeasurement-KalmanState);
  KalmanUncertainty = (1-KalmanGain) * KalmanUncertainty;
  Kalman1DOutput[0] = KalmanState;
  Kalman1DOutput[1] = KalmanUncertainty;
}
void kalman_2d(void);
void define_kalman_2d(void);

int SetValue(int value, int inMin, int inMax, int outMin, int outMax)
{
	if (value < inMin) return outMin;
	else if (value > inMax) return outMax;
	else return value;
}

void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin){

	if ( GPIO_Pin == GPIO_PIN_15){
		tick = __HAL_TIM_GET_COUNTER(&htim2);
		__HAL_TIM_SET_COUNTER(&htim2,0);

		if ( tick < 2100){
			ch[pulse] = SetValue(tick, 1030, 1950, 1000, 2000);;
			pulse++;
		}
		else{
			__HAL_TIM_SET_COUNTER(&htim2,0);
			pulse =0;
		}
	}
}


void MPU6050_Init (void)
{
	uint8_t check;
	uint8_t Data;

	// check device ID WHO_AM_I

	HAL_I2C_Mem_Read (&hi2c1, MPU6050_ADDR,WHO_AM_I_REG,1, &check, 1,1000);

	if (check == 104)  // 0x68 will be returned by the sensor if everything goes well
	{
		// power management register 0X6B we should write all 0's to wake the sensor up
		Data = 0;
		if(HAL_I2C_Mem_Write(&hi2c1, MPU6050_ADDR, PWR_MGMT_1_REG, 1,&Data, 1,1000)!= HAL_OK)
			fault_dec();
		// config 1KHz Digital Low Pass Filter 10Hz
		Data = 0x05;
		if(HAL_I2C_Mem_Write(&hi2c1, MPU6050_ADDR, CONFIG_REG, 1,&Data, 1,1000)!= HAL_OK)
			fault_dec();
		
		// Set DATA RATE of 1KHz by writing SMPLRT_DIV register
		Data = 0x07;
		if(HAL_I2C_Mem_Write(&hi2c1, MPU6050_ADDR, SMPLRT_DIV_REG, 1, &Data, 1,1000)!= HAL_OK)
			fault_dec();

		// Set accelerometer configuration in ACCEL_CONFIG Register
		// XA_ST=0,YA_ST=0,ZA_ST=0, FS_SEL=0 -> � 8g
		Data = 0x10;
		if(HAL_I2C_Mem_Write(&hi2c1, MPU6050_ADDR, ACCEL_CONFIG_REG, 1, &Data, 1,1000)!= HAL_OK)
			fault_dec();

		// Set Gyroscopic configuration in GYRO_CONFIG Register
		// XG_ST=0,YG_ST=0,ZG_ST=0, FS_SEL=0 -> � 500 �/s
		Data = 0x08;
		if(HAL_I2C_Mem_Write(&hi2c1, MPU6050_ADDR, GYRO_CONFIG_REG, 1, &Data, 1,1000)!= HAL_OK)
			fault_dec();
	}
}


HAL_StatusTypeDef MPU6050_Read_Data (void)
{
	uint8_t Rec_Data_Acc[6];
	uint8_t Rec_Data_Gyro[6];

	// Read 6 BYTES of data starting from ACCEL_XOUT_H register

	if(HAL_I2C_Mem_Read (&hi2c1, MPU6050_ADDR, ACCEL_XOUT_H_REG,1,Rec_Data_Acc,6,20) != HAL_OK)
		return HAL_ERROR;

	Accel_X_RAW = (int16_t)(Rec_Data_Acc[0] << 8 | Rec_Data_Acc [1]);
	Accel_Y_RAW = (int16_t)(Rec_Data_Acc[2] << 8 | Rec_Data_Acc [3]);
	Accel_Z_RAW = (int16_t)(Rec_Data_Acc[4] << 8 | Rec_Data_Acc [5]);

	/*** convert the RAW values into acceleration in 'g'
	     we have to divide according to the Full scale value set in FS_SEL
	     I have configured FS_SEL = 0. So I am dividing by 16384.0
	     for more details check ACCEL_CONFIG Register              ****/

	Ax = Accel_X_RAW/4096.0;
	Ay = Accel_Y_RAW/4096.0;
	Az = Accel_Z_RAW/4096.0;
	
	if(HAL_I2C_Mem_Read (&hi2c1, MPU6050_ADDR, GYRO_XOUT_H_REG,1,Rec_Data_Gyro,6,20)!= HAL_OK)
		return HAL_ERROR;
	Gyro_X_RAW = (int16_t)(Rec_Data_Gyro[0] << 8 | Rec_Data_Gyro [1]);
	Gyro_Y_RAW = (int16_t)(Rec_Data_Gyro[2] << 8 | Rec_Data_Gyro [3]);
	Gyro_Z_RAW = (int16_t)(Rec_Data_Gyro[4] << 8 | Rec_Data_Gyro [5]);

	/*** convert the RAW values into dps (�/s)
	     we have to divide according to the Full scale value set in FS_SEL
	     I have configured FS_SEL = 0. So I am dividing by 131.0
	     for more details check GYRO_CONFIG Register              ****/

	Gx = Gyro_X_RAW/65.5;
	Gy = Gyro_Y_RAW/65.5;
	Gz = Gyro_Z_RAW/65.5;
	
	AngleRoll = atan(Ay/(sqrt(Ax*Ax + Az*Az)))*180/pi;
	AnglePitch = -atan(Ax/(sqrt(Ay*Ay + Az*Az)))*180/pi;
	return HAL_OK;
}

void Calib_Gyro(void);
void Reset_MPU6050(void);
void Calib_BMP280(void);

void pid_equation(float Error, float P , float I, float D, float PrevError, float PrevIterm) {
  // T = 4ms chu ky lay mau, 250Hz
  // T = 20ms chu ky lay mau, 50Hz
  float Pterm=P*Error;
  float Iterm=PrevIterm+I*(Error+PrevError)*0.004/2;
  if (Iterm > 400) Iterm=400;
  else if (Iterm <-400) Iterm=-400;
  float Dterm=D*(Error-PrevError)/0.004;
  float PIDOutput= Pterm+Iterm+Dterm;
  if (PIDOutput>400) PIDOutput=400;
  else if (PIDOutput <-400) PIDOutput=-400;
  PIDReturn[0]=PIDOutput;
  PIDReturn[1]=Error;
  PIDReturn[2]=Iterm;
}

void reset_pid(void) {
  PrevErrorRateRoll=0; PrevErrorRatePitch=0; PrevErrorRateYaw=0;
  PrevItermRateRoll=0; PrevItermRatePitch=0; PrevItermRateYaw=0;
  PrevErrorAngleRoll=0; PrevErrorAnglePitch=0;    
  PrevItermAngleRoll=0; PrevItermAnglePitch=0;
}

void setupSensor(void)
{
    twiSend(address, 0x1E,1); //just send 1 byte that tells MS5611 to reset
    HAL_Delay(20); //delay 10 mS needed for device to execute reset
    for (int i=1;i<=6;i++)
    {
    twiReceive(address, 0xA0+i*2, 2); //read all 14 bytes for callibration data from PROM
    //printMsg("b0= 0x%x, b1= 0x%x, b2= 0x%x \n",buffer[0], buffer[1], buffer[2]); //for debug purposes
    HAL_Delay(5); //at least 40 uS
    calibrationData[i] = buffer[0]<<8|buffer[1]; //pair of bytes goes into each element of callibrationData[i], global variables, 14 uint8_t into 7 uint16_t
  }
}
int getPressure(void)
{
    D1=0;D2=0;
    twiSend(address, 0x48,1); //set D1 OSR=4096 (overscan, maximum) 0x48
    HAL_Delay(1);//must be 15 mS or more
    twiReceive(address, 0x00, 3); //initiate and read ADC data, 3 bytes
    //printMsg("b0= 0x%x, b1= 0x%x, b2= 0x%x ===========\n",buffer[0], buffer[1], buffer[2]); //for debug purposes
    D1 = D1<<8 | buffer[0]; //shifting first MSB byte left
    D1 = D1<<8 | buffer[1]; //another byte
    D1 = D1<<8 | buffer[2]; //LSB byte last
    twiSend(address, 0x58,1); //set D2 OSR=4096 (overscan, maximum) 0x58
    HAL_Delay(1);//must be 15 mS or more
    twiReceive(address, 0x00, 3); //initiate and read ADC data, 3 bytes
    D2 = D2<<8 | buffer[0]; //shifting first MSB byte left
    D2 = D2<<8 | buffer[1]; //another byte
    D2 = D2<<8 | buffer[2]; //LSB byte last

    dT = D2 - ((int)calibrationData[5] << 8);
  TEMP = (2000 + (((int64_t)dT * (int64_t)calibrationData[6]) >> 23)); //temperature before second order compensation
  if (TEMP<2000)  //if temperature of the sensor goes below 20°C, it activates "second order temperature compensation"
    {
      T2=pow(dT,2)/2147483648;
      OFF2=5*pow((TEMP-2000),2)/2;
      SENS2=5*pow((TEMP-2000),2)/4;
      if (TEMP<-1500) //if temperature of the sensor goes even lower, below -15°C, then additional math is utilized
        {
          OFF2=OFF2+7*pow((TEMP+1500),2);
          SENS2=SENS2+11*pow((TEMP+1500),2)/2;
        }
    }
    else
      {
          T2=0;
          OFF2=0;
          SENS2=0;
      }
  TEMP = ((2000 + (((int64_t)dT * (int64_t)calibrationData[6]) >> 23))-T2); //second order compensation included
  OFF = (((unsigned int)calibrationData[2] << 16) + (((int64_t)calibrationData[4] * dT) >> 7)-OFF2); //second order compensation included
  SENS = (((unsigned int)calibrationData[1] << 15) + (((int64_t)calibrationData[3] * dT) >> 8)-SENS2); //second order compensation included
  P = (((D1 * SENS) >> 21) - OFF) >> 15;
  return P; //returns back pressure P
}
/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_DMA_Init(void);
static void MX_I2C1_Init(void);
static void MX_TIM1_Init(void);
static void MX_TIM2_Init(void);
static void MX_TIM3_Init(void);
static void MX_TIM4_Init(void);
static void MX_USART2_UART_Init(void);
static void MX_I2C2_Init(void);
static void MX_USART3_UART_Init(void);
static void MX_SPI3_Init(void);
/* USER CODE BEGIN PFP */
int Receive_Throttle_Min(void);
/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */
//float Temperature, Pressure, Humidity, Altitude;
int ret;

void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{
	if(htim->Instance == TIM4)
	{	
		Send_Data_To_Gui();
	}
}

void HAL_UARTEx_RxEventCallback(UART_HandleTypeDef *huart, uint16_t Size)
{   
	HAL_UARTEx_ReceiveToIdle_DMA(&huart2, Rx_buff, sizeof(Rx_buff));
  __HAL_DMA_DISABLE_IT(&hdma_usart2_rx, DMA_IT_HT);
	size_RxData = Size;
	if ((Rx_buff[0] == 0xAA) && (Rx_buff[1] == 0xFF) && (Rx_buff[2] == size_RxData))
	{
		memset(Tx_buff, 0, sizeof(Tx_buff));
		memcpy(Rx_Data, Rx_buff, sizeof(Rx_buff));
		memset(Rx_buff, 0, sizeof(Rx_buff));
		Tx_buff[0] = 'B'; Tx_buff[1] = 'E';Tx_buff[18] = '\r'; Tx_buff[19] = '\n';
		HAL_UART_Transmit_DMA(&huart2, (uint8_t*)Tx_buff,20);
		//memset(Tx_buff, 0, sizeof(Tx_buff));	
	}
	if ((Rx_buff[0] == 0xCC) && (Rx_buff[1] == 0xDD))
	{
		memset(Tx_buff, 0, sizeof(Tx_buff));
		Flag_Plot = Rx_buff[2];
		memset(Rx_buff, 0, sizeof(Rx_buff));		
		if (Flag_Plot){ Tx_buff[0] = 'P'; Tx_buff[1] = 'L';}
		else{Tx_buff[0] = 'S'; Tx_buff[1] = 'T'; }		  
		Tx_buff[18] = '\r'; Tx_buff[19] = '\n';
		HAL_UART_Transmit_DMA(&huart2, (uint8_t*)Tx_buff,20);
		//memset(Tx_buff, 0, sizeof(Tx_buff));	
	}
	// gan gia tri pid vao
	memcpy(&PAngleRoll, &Rx_Data[3], sizeof(float));
	memcpy(&IAngleRoll, &Rx_Data[7], sizeof(float));
	memcpy(&DAngleRoll, &Rx_Data[11], sizeof(float));
	
	memcpy(&PAnglePitch, &Rx_Data[15], sizeof(float));
	memcpy(&IAnglePitch, &Rx_Data[19], sizeof(float));
	memcpy(&DAnglePitch, &Rx_Data[23], sizeof(float));
	
	memcpy(&PRateRoll, &Rx_Data[27], sizeof(float));
	memcpy(&IRateRoll, &Rx_Data[31], sizeof(float));
	memcpy(&DRateRoll, &Rx_Data[35], sizeof(float));
	
	memcpy(&PRatePitch, &Rx_Data[39], sizeof(float));
	memcpy(&IRatePitch, &Rx_Data[43], sizeof(float));
	memcpy(&DRatePitch, &Rx_Data[47], sizeof(float));
	
	memcpy(&PRateYaw, &Rx_Data[51], sizeof(float));
    memcpy(&IRateYaw, &Rx_Data[55], sizeof(float));
	memcpy(&DRateYaw, &Rx_Data[59], sizeof(float));
}
/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{

  /* USER CODE BEGIN 1 */

  /* USER CODE END 1 */

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_DMA_Init();
  MX_I2C1_Init();
  MX_TIM1_Init();
  MX_TIM2_Init();
  MX_TIM3_Init();
  MX_TIM4_Init();
  MX_USART2_UART_Init();
  MX_I2C2_Init();
  MX_USART3_UART_Init();
  MX_SPI3_Init();
  /* USER CODE BEGIN 2 */
//  BMP280_WakeUP();
//  HAL_Delay(1000);
//  ret = BMP280_Config(OSRS_16, OSRS_16, OSRS_OFF, MODE_NORMAL, T_SB_0p5, IIR_16);
//  HAL_Delay(100);
//  while(ret)
//  {
//	HAL_I2C_DeInit(&hi2c2);
//	MX_I2C2_Init();
//    BMP280_WakeUP();
//    HAL_Delay(100);
//  	ret = BMP280_Config(OSRS_16, OSRS_16, OSRS_OFF, MODE_NORMAL, T_SB_0p5, IIR_16);
//  }
  //Calib_BMP280();
  //define_kalman_2d();
  setupSensor();
  HAL_TIM_Base_Start_IT(&htim4);
  HAL_UARTEx_ReceiveToIdle_DMA(&huart2, Rx_buff, sizeof(Rx_buff));
  __HAL_DMA_DISABLE_IT(&hdma_usart2_rx, DMA_IT_HT);
  HAL_TIM_Base_Start(&htim2);
  HAL_TIM_Base_Start(&htim1);
  HAL_TIM_PWM_Start(&htim3, TIM_CHANNEL_1);
  HAL_TIM_PWM_Start(&htim3, TIM_CHANNEL_2);
  HAL_TIM_PWM_Start(&htim3, TIM_CHANNEL_3);
  HAL_TIM_PWM_Start(&htim3, TIM_CHANNEL_4);
  HAL_Delay(20);
  __HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_1,0);
  __HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_2,0);
  __HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_3,0);
  __HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_4,0);
  MX_I2C1_Init();
  HAL_Delay(20);
  MPU6050_Init();
  HAL_Delay(1000);
  Calib_Gyro();
  HAL_Delay(50);
  ch[0]=1500;      //Roll
  ch[1]=1500;      //Pitch
  ch[2]=1000;      //Throttle
  ch[3]=1500;      //Yaw
  while(Receive_Throttle_Min() == 0);
  HAL_Delay(20);
  loop_timer = TIM1->CNT;
  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
    /* USER CODE END WHILE */

    /* USER CODE BEGIN 3 */
	  if(MPU6050_Read_Data() != HAL_OK)
	  {
	  	Reset_MPU6050();
	  }
	  //BMP280_Measure();
	  pressure = getPressure();
	  Altitude = 44330.0f*(1-powf((pressure/101325.0f),(1.0f/5.257f)));
	  receiver_input_channel_1 = ch[0];
	  receiver_input_channel_2 = ch[1];
	  receiver_input_channel_3 = ch[2]; // chan ga(Throttle)
	  receiver_input_channel_4 = ch[3];
		
	  Gx -= Gyro_X_Calib;
	  Gy -= Gyro_Y_Calib;
	  Gz -= Gyro_Z_Calib;

//      AccZInertial = -sin(AnglePitch*(pi/180))*Ax + cos(AnglePitch*(pi/180))*sin(AngleRoll*(pi/180))*Ay + cos(AnglePitch*(pi/180))*cos(AngleRoll*(pi/180))*Az;
//	  AccZInertial = (AccZInertial-1)*9.81;  //
//      VelocityVertical = VelocityVertical + AccZInertial*0.004;
//	  Altitude -= Altitude_Calib;
//	  define_kalman_2d();
//	  kalman_2d();


	  kalman_1d(KalmanAngleRoll, KalmanUncertaintyAngleRoll, Gx, AngleRoll);
      	  KalmanAngleRoll = Kalman1DOutput[0];
      	  KalmanUncertaintyAngleRoll = Kalman1DOutput[1];

	  kalman_1d(KalmanAnglePitch, KalmanUncertaintyAnglePitch, Gy, AnglePitch);
	  	  KalmanAnglePitch = Kalman1DOutput[0];
	  	  KalmanUncertaintyAnglePitch = Kalman1DOutput[1];
				 
	/*------------------------------------------------------------------------*/
// /* 1 vong rate angle */									
//	DesiredRateRoll = 0.10*(receiver_input_channel_1-1500);  // -50do/s den 50do/s
//  DesiredRatePitch = 0.10*(receiver_input_channel_2-1500); // -50do/s den 50do/s
//  InputThrottle = receiver_input_channel_3;
//  DesiredRateYaw = 0.1*(receiver_input_channel_4-1500);
//	
//  ErrorRateRoll=DesiredRateRoll-Gx;
//  ErrorRatePitch=DesiredRatePitch-Gy;
//  ErrorRateYaw=DesiredRateYaw-Gz;
//	
//  pid_equation(ErrorRateRoll, PRateRoll, IRateRoll, DRateRoll, PrevErrorRateRoll, PrevItermRateRoll);
//       InputRoll=PIDReturn[0];
//       PrevErrorRateRoll=PIDReturn[1]; 
//       PrevItermRateRoll=PIDReturn[2];
//			 
//  pid_equation(ErrorRatePitch, PRatePitch,IRatePitch, DRatePitch, PrevErrorRatePitch, PrevItermRatePitch);
//       InputPitch=PIDReturn[0]; 
//       PrevErrorRatePitch=PIDReturn[1]; 
//       PrevItermRatePitch=PIDReturn[2];
//			 
//  pid_equation(ErrorRateYaw, PRateYaw,IRateYaw, DRateYaw, PrevErrorRateYaw, PrevItermRateYaw);
//       InputYaw=PIDReturn[0]; 
//       PrevErrorRateYaw=PIDReturn[1]; 
//       PrevItermRateYaw=PIDReturn[2];

	DesiredAngleRoll = 0.10*(receiver_input_channel_1-1500);  // -50do den 50do
    DesiredAnglePitch = 0.10*(receiver_input_channel_2-1500); // -50do den 50do
    InputThrottle = receiver_input_channel_3;
    DesiredRateYaw = 0.1*(receiver_input_channel_4-1500);
	
	ErrorAngleRoll = DesiredAngleRoll - KalmanAngleRoll;
	ErrorAnglePitch = DesiredAnglePitch - KalmanAnglePitch;
	
	pid_equation(ErrorAngleRoll,PAngleRoll,IAngleRoll,DAngleRoll,PrevErrorAngleRoll,PrevItermAngleRoll);
	DesiredRateRoll = PIDReturn[0];
	PrevErrorAngleRoll = PIDReturn[1];
	PrevItermAngleRoll = PIDReturn[2];
	
	pid_equation(ErrorAnglePitch,PAnglePitch,IAnglePitch,DAnglePitch,PrevErrorAnglePitch,PrevItermAnglePitch);
	DesiredRatePitch = PIDReturn[0];
	PrevErrorAnglePitch = PIDReturn[1];
	PrevItermAnglePitch = PIDReturn[2];
	
	ErrorRateRoll = DesiredRateRoll - Gx;
	ErrorRatePitch = DesiredRatePitch - Gy;
	ErrorRateYaw = DesiredRateYaw - Gz;
	
  pid_equation(ErrorRateRoll, PRateRoll, IRateRoll, DRateRoll, PrevErrorRateRoll, PrevItermRateRoll);
       InputRoll=PIDReturn[0];
       PrevErrorRateRoll=PIDReturn[1]; 
       PrevItermRateRoll=PIDReturn[2];
			 
  pid_equation(ErrorRatePitch, PRatePitch,IRatePitch, DRatePitch, PrevErrorRatePitch, PrevItermRatePitch);
       InputPitch=PIDReturn[0]; 
       PrevErrorRatePitch=PIDReturn[1]; 
       PrevItermRatePitch=PIDReturn[2];
			 
  pid_equation(ErrorRateYaw, PRateYaw,IRateYaw, DRateYaw, PrevErrorRateYaw, PrevItermRateYaw);
       InputYaw=PIDReturn[0]; 
       PrevErrorRateYaw=PIDReturn[1]; 
       PrevItermRateYaw=PIDReturn[2];
		 
  if (InputThrottle > 1800) InputThrottle = 1800;	
  MotorInput1= (InputThrottle-InputRoll-InputPitch-InputYaw);
  MotorInput2= (InputThrottle-InputRoll+InputPitch+InputYaw);
  MotorInput3= (InputThrottle+InputRoll+InputPitch-InputYaw);
  MotorInput4= (InputThrottle+InputRoll-InputPitch+InputYaw);
	
  if (MotorInput1 > 1999)MotorInput1 = 1999;
  if (MotorInput2 > 1999)MotorInput2 = 1999; 
  if (MotorInput3 > 1999)MotorInput3 = 1999; 
  if (MotorInput4 > 1999)MotorInput4 = 1999;
  
  int ThrottleIdle = 1180;
  if (MotorInput1 < ThrottleIdle) MotorInput1 = ThrottleIdle;
  if (MotorInput2 < ThrottleIdle) MotorInput2 = ThrottleIdle;
  if (MotorInput3 < ThrottleIdle) MotorInput3 = ThrottleIdle;
  if (MotorInput4 < ThrottleIdle) MotorInput4 = ThrottleIdle;
  
  int ThrottleCutOff = 1000;
  if (receiver_input_channel_3 < 1050) 
  {  
		MotorInput1=ThrottleCutOff; 
    MotorInput2=ThrottleCutOff;
    MotorInput3=ThrottleCutOff; 
    MotorInput4=ThrottleCutOff;
    reset_pid();
  }
	
		__HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_1,MotorInput1);
	 	__HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_2,MotorInput2);
	 	__HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_3,MotorInput3);
	 	__HAL_TIM_SET_COMPARE(&htim3,TIM_CHANNEL_4,MotorInput4);
	
									/*------------------------------------------------------------*/
	// chu ky lay mau T = 4ms , f = 250Hz
	// chu ky lay mau T = 20ms, f = 50Hz
	while ( abs(__HAL_TIM_GET_COUNTER(&htim1) - loop_timer) < 4000 ) ;
	 		 __HAL_TIM_SET_COUNTER(&htim1,0);
	 		 loop_timer = TIM1->CNT;
  }
  /* USER CODE END 3 */
}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

  /** Configure the main internal regulator output voltage
  */
  __HAL_RCC_PWR_CLK_ENABLE();
  __HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE1);

  /** Initializes the RCC Oscillators according to the specified parameters
  * in the RCC_OscInitTypeDef structure.
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSI;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.HSICalibrationValue = RCC_HSICALIBRATION_DEFAULT;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSI;
  RCC_OscInitStruct.PLL.PLLM = 8;
  RCC_OscInitStruct.PLL.PLLN = 168;
  RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV2;
  RCC_OscInitStruct.PLL.PLLQ = 4;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }

  /** Initializes the CPU, AHB and APB buses clocks
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV4;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV2;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_5) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief I2C1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_I2C1_Init(void)
{

  /* USER CODE BEGIN I2C1_Init 0 */

  /* USER CODE END I2C1_Init 0 */

  /* USER CODE BEGIN I2C1_Init 1 */

  /* USER CODE END I2C1_Init 1 */
  hi2c1.Instance = I2C1;
  hi2c1.Init.ClockSpeed = 400000;
  hi2c1.Init.DutyCycle = I2C_DUTYCYCLE_2;
  hi2c1.Init.OwnAddress1 = 0;
  hi2c1.Init.AddressingMode = I2C_ADDRESSINGMODE_7BIT;
  hi2c1.Init.DualAddressMode = I2C_DUALADDRESS_DISABLE;
  hi2c1.Init.OwnAddress2 = 0;
  hi2c1.Init.GeneralCallMode = I2C_GENERALCALL_DISABLE;
  hi2c1.Init.NoStretchMode = I2C_NOSTRETCH_DISABLE;
  if (HAL_I2C_Init(&hi2c1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN I2C1_Init 2 */

  /* USER CODE END I2C1_Init 2 */

}

/**
  * @brief I2C2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_I2C2_Init(void)
{

  /* USER CODE BEGIN I2C2_Init 0 */

  /* USER CODE END I2C2_Init 0 */

  /* USER CODE BEGIN I2C2_Init 1 */

  /* USER CODE END I2C2_Init 1 */
  hi2c2.Instance = I2C2;
  hi2c2.Init.ClockSpeed = 400000;
  hi2c2.Init.DutyCycle = I2C_DUTYCYCLE_2;
  hi2c2.Init.OwnAddress1 = 0;
  hi2c2.Init.AddressingMode = I2C_ADDRESSINGMODE_7BIT;
  hi2c2.Init.DualAddressMode = I2C_DUALADDRESS_DISABLE;
  hi2c2.Init.OwnAddress2 = 0;
  hi2c2.Init.GeneralCallMode = I2C_GENERALCALL_DISABLE;
  hi2c2.Init.NoStretchMode = I2C_NOSTRETCH_DISABLE;
  if (HAL_I2C_Init(&hi2c2) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN I2C2_Init 2 */

  /* USER CODE END I2C2_Init 2 */

}

/**
  * @brief SPI3 Initialization Function
  * @param None
  * @retval None
  */
static void MX_SPI3_Init(void)
{

  /* USER CODE BEGIN SPI3_Init 0 */

  /* USER CODE END SPI3_Init 0 */

  /* USER CODE BEGIN SPI3_Init 1 */

  /* USER CODE END SPI3_Init 1 */
  /* SPI3 parameter configuration*/
  hspi3.Instance = SPI3;
  hspi3.Init.Mode = SPI_MODE_MASTER;
  hspi3.Init.Direction = SPI_DIRECTION_2LINES;
  hspi3.Init.DataSize = SPI_DATASIZE_8BIT;
  hspi3.Init.CLKPolarity = SPI_POLARITY_LOW;
  hspi3.Init.CLKPhase = SPI_PHASE_1EDGE;
  hspi3.Init.NSS = SPI_NSS_SOFT;
  hspi3.Init.BaudRatePrescaler = SPI_BAUDRATEPRESCALER_2;
  hspi3.Init.FirstBit = SPI_FIRSTBIT_MSB;
  hspi3.Init.TIMode = SPI_TIMODE_DISABLE;
  hspi3.Init.CRCCalculation = SPI_CRCCALCULATION_DISABLE;
  hspi3.Init.CRCPolynomial = 10;
  if (HAL_SPI_Init(&hspi3) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN SPI3_Init 2 */

  /* USER CODE END SPI3_Init 2 */

}

/**
  * @brief TIM1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM1_Init(void)
{

  /* USER CODE BEGIN TIM1_Init 0 */

  /* USER CODE END TIM1_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM1_Init 1 */

  /* USER CODE END TIM1_Init 1 */
  htim1.Instance = TIM1;
  htim1.Init.Prescaler = 167;
  htim1.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim1.Init.Period = 65535;
  htim1.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim1.Init.RepetitionCounter = 0;
  htim1.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim1, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim1, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM1_Init 2 */

  /* USER CODE END TIM1_Init 2 */

}

/**
  * @brief TIM2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM2_Init(void)
{

  /* USER CODE BEGIN TIM2_Init 0 */

  /* USER CODE END TIM2_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM2_Init 1 */

  /* USER CODE END TIM2_Init 1 */
  htim2.Instance = TIM2;
  htim2.Init.Prescaler = 83;
  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim2.Init.Period = 4294967295-1;
  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim2) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim2, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM2_Init 2 */

  /* USER CODE END TIM2_Init 2 */

}

/**
  * @brief TIM3 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM3_Init(void)
{

  /* USER CODE BEGIN TIM3_Init 0 */

  /* USER CODE END TIM3_Init 0 */

  TIM_MasterConfigTypeDef sMasterConfig = {0};
  TIM_OC_InitTypeDef sConfigOC = {0};

  /* USER CODE BEGIN TIM3_Init 1 */

  /* USER CODE END TIM3_Init 1 */
  htim3.Instance = TIM3;
  htim3.Init.Prescaler = 83;
  htim3.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim3.Init.Period = 19999;
  htim3.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim3.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_PWM_Init(&htim3) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim3, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigOC.OCMode = TIM_OCMODE_PWM1;
  sConfigOC.Pulse = 0;
  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
  sConfigOC.OCFastMode = TIM_OCFAST_ENABLE;
  if (HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_2) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_3) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_4) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM3_Init 2 */

  /* USER CODE END TIM3_Init 2 */
  HAL_TIM_MspPostInit(&htim3);

}

/**
  * @brief TIM4 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM4_Init(void)
{

  /* USER CODE BEGIN TIM4_Init 0 */

  /* USER CODE END TIM4_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM4_Init 1 */

  /* USER CODE END TIM4_Init 1 */
  htim4.Instance = TIM4;
  htim4.Init.Prescaler = 8399;
  htim4.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim4.Init.Period = 1249;
  htim4.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim4.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim4) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim4, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim4, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM4_Init 2 */

  /* USER CODE END TIM4_Init 2 */

}

/**
  * @brief USART2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART2_UART_Init(void)
{

  /* USER CODE BEGIN USART2_Init 0 */

  /* USER CODE END USART2_Init 0 */

  /* USER CODE BEGIN USART2_Init 1 */

  /* USER CODE END USART2_Init 1 */
  huart2.Instance = USART2;
  huart2.Init.BaudRate = 115200;
  huart2.Init.WordLength = UART_WORDLENGTH_8B;
  huart2.Init.StopBits = UART_STOPBITS_1;
  huart2.Init.Parity = UART_PARITY_NONE;
  huart2.Init.Mode = UART_MODE_TX_RX;
  huart2.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart2.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart2) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART2_Init 2 */

  /* USER CODE END USART2_Init 2 */

}

/**
  * @brief USART3 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART3_UART_Init(void)
{

  /* USER CODE BEGIN USART3_Init 0 */

  /* USER CODE END USART3_Init 0 */

  /* USER CODE BEGIN USART3_Init 1 */

  /* USER CODE END USART3_Init 1 */
  huart3.Instance = USART3;
  huart3.Init.BaudRate = 115200;
  huart3.Init.WordLength = UART_WORDLENGTH_8B;
  huart3.Init.StopBits = UART_STOPBITS_1;
  huart3.Init.Parity = UART_PARITY_NONE;
  huart3.Init.Mode = UART_MODE_TX_RX;
  huart3.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart3.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart3) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART3_Init 2 */

  /* USER CODE END USART3_Init 2 */

}

/**
  * Enable DMA controller clock
  */
static void MX_DMA_Init(void)
{

  /* DMA controller clock enable */
  __HAL_RCC_DMA1_CLK_ENABLE();

  /* DMA interrupt init */
  /* DMA1_Stream0_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Stream0_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Stream0_IRQn);
  /* DMA1_Stream1_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Stream1_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Stream1_IRQn);
  /* DMA1_Stream3_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Stream3_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Stream3_IRQn);
  /* DMA1_Stream5_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Stream5_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Stream5_IRQn);
  /* DMA1_Stream6_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Stream6_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Stream6_IRQn);
  /* DMA1_Stream7_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Stream7_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA1_Stream7_IRQn);

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};
/* USER CODE BEGIN MX_GPIO_Init_1 */
/* USER CODE END MX_GPIO_Init_1 */

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOH_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();
  __HAL_RCC_GPIOD_CLK_ENABLE();
  __HAL_RCC_GPIOC_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOD, GPIO_PIN_0|GPIO_PIN_1, GPIO_PIN_RESET);

  /*Configure GPIO pin : PB15 */
  GPIO_InitStruct.Pin = GPIO_PIN_15;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_RISING;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

  /*Configure GPIO pins : PD0 PD1 */
  GPIO_InitStruct.Pin = GPIO_PIN_0|GPIO_PIN_1;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOD, &GPIO_InitStruct);

  /* EXTI interrupt init*/
  HAL_NVIC_SetPriority(EXTI15_10_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI15_10_IRQn);

/* USER CODE BEGIN MX_GPIO_Init_2 */
/* USER CODE END MX_GPIO_Init_2 */
}

/* USER CODE BEGIN 4 */
int Receive_Throttle_Min(void)
{
	if(ch[2]<1020) return 1;
	else return 0;
}

void Calib_Gyro(void){
	for( int i = 0; i < 2000; i++){
		if(MPU6050_Read_Data() != HAL_OK)
		{
			Reset_MPU6050();
		}
		Gyro_X_Calib += Gx;
		Gyro_Y_Calib += Gy;
		Gyro_Z_Calib += Gz;
		HAL_Delay(6);
	}
	Gyro_X_Calib /= 2000;
	Gyro_Y_Calib /= 2000;
	Gyro_Z_Calib /= 2000;
}
void Calib_BMP280(void)
{
	for (int i = 0;i < 2000; i++)
	{
		BMP280_Measure();
		Altitude_Calib += Altitude;
		HAL_Delay(1);
	}
	Altitude_Calib /= 2000;
}

void Reset_MPU6050(void)
{
	HAL_I2C_DeInit(&hi2c1);
	HAL_I2C_Init(&hi2c1);
	MX_I2C1_Init();
	MPU6050_Init();
	MPU6050_Read_Data();
}
//void kalman_2d(void)
//{
//	float S_temp[2][1];
//	float P_temp[2][2];
//
//	Acc[0][0] = AccZInertial;
//	// S = F*S + G*Acc;
//	S_temp[0][0] = S[0][0]; S_temp[1][0] = S[1][0];
//	S[0][0] = F[0][0]*S[0][0] + F[0][1]*S[1][0] + G[0][0]*Acc[0][0];
//	S[1][0] = F[1][0]*S[0][0] + F[1][1]*S[1][0] + G[1][0]*Acc[0][0];
//	//P = F*P*~F + Q;
//	P_temp[0][0] = P[0][0]; P_temp[0][1] = P[0][1]; P_temp[1][0] = P[1][0]; P_temp[1][1] = P[1][1];
//	P[0][0] = (F[0][0]*P_temp[0][0]+F[0][1]*P_temp[1][0])*F[0][0] + (F[0][0]*P_temp[0][1]+F[0][1]*P_temp[1][1])*F[0][1] + Q[0][0];
//	P[0][1] = (F[0][0]*P_temp[0][0]+F[0][1]*P_temp[1][0])*F[1][0] + (F[0][0]*P_temp[0][1]+F[0][1]*P_temp[1][1])*F[1][1] + Q[0][1];
//	P[1][0] = (F[1][0]*P_temp[0][0]+F[1][1]*P_temp[1][0])*F[0][0] + (F[1][0]*P_temp[0][1]+F[1][1]*P_temp[1][1])*F[0][1] + Q[1][0];
//	P[1][1] = (F[1][0]*P_temp[0][0]+F[1][1]*P_temp[1][0])*F[1][0] + (F[1][0]*P_temp[0][1]+F[1][1]*P_temp[1][1])*F[1][1] + Q[1][1];
//	//L = H*P*~H + R;
//	L[0][0] = H[0][0]*(H[0][0]*P[0][0]+H[0][1]*P[1][0]) + H[0][1]*(H[0][0]*P[0][1]+H[0][1]*P[1][1]) + R[0][0];
//	//K = P*~H*Invert(L);
//	K[0][0] = (P[0][0]*H[0][0] + P[0][1]*H[0][1])/(L[0][0]);
//	K[1][0] = (P[1][0]*H[0][0] + P[1][1]*H[0][1])/(L[0][0]);
//
//	M[0][0] = Altitude;
//	//S = S + K*(M-H*S);
//	S_temp[0][0] = S[0][0]; S_temp[1][0] = S[1][0];
//	S[0][0] = S_temp[0][0] + K[0][0]*(M[0][0] - (H[0][0]*S_temp[0][0] + H[0][1]*S_temp[1][0]));
//	S[1][0] = S_temp[1][0] + K[1][0]*(M[0][0] - (H[0][0]*S_temp[0][0] + H[0][1]*S_temp[1][0]));
//	AltitudeKalman = S[0][0];
//	VelocityVerticalKalman = S[1][0];
//	//P = (I-K*H)*P;
//	P_temp[0][0] = P[0][0]; P_temp[0][1] = P[0][1]; P_temp[1][0] = P[1][0]; P_temp[1][1] = P[1][1];
//	P[0][0] = (I[0][0]-K[0][0]*H[0][0])*P_temp[0][0] + (I[0][1]-K[0][0]*H[0][1])*P_temp[1][0];
//	P[0][1] = (I[0][0]-K[0][0]*H[0][0])*P_temp[0][1] + (I[0][1]-K[0][0]*H[0][1])*P_temp[1][1];
//	P[1][0] = (I[1][0]-K[1][0]*H[0][0])*P_temp[0][0] + (I[1][1]-K[1][0]*H[0][1])*P_temp[1][0];
//	P[1][1] = (I[1][0]-K[1][0]*H[0][0])*P_temp[0][1] + (I[1][1]-K[1][0]*H[0][1])*P_temp[1][1];
//}
//void define_kalman_2d(void)
//{
//	F[0][0] = 1; F[0][1] = 0.004;F[1][0] = 0;F[1][1] = 1;
//	G[0][0] = 0.5*0.004*0.004; G[1][0] = 0.004;
//	H[0][0] = 1; H[0][1] = 0;
//	I[0][0] = 1; I[0][1] = 0;I[1][0] = 0;I[1][1] = 1;
//	//Q = G*~G*10*10;  10 cm/m^2
//	Q[0][0] = G[0][0]* G[0][0];
//	Q[0][1] = G[0][0]* G[1][0];
//	Q[1][0] = G[1][0]* G[0][0];
//	Q[1][1] = G[1][0]* G[1][0];
//
//	R[0][0] = 30*30;
//	P[0][0] = 0;P[0][1] = 0;P[1][0] = 0;P[1][1] = 0;
//}

void matrix_multiply(float A[2][2], float B[2][2], float result[2][2]) {
    for (int i = 0; i < 2; i++) {
        for (int j = 0; j < 2; j++) {
            result[i][j] = 0;
            for (int k = 0; k < 2; k++) {
                result[i][j] += A[i][k] * B[k][j];
            }
        }
    }
}

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  __disable_irq();
  while (1)
  {
  }
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */
