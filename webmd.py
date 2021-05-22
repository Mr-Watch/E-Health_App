from time import sleep 
import json
from selenium.webdriver.firefox.options import Options
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.common.keys import Keys

from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException
from selenium.common.exceptions import StaleElementReferenceException

options = Options()
options.headless = False
options.page_load_strategy = 'none'
driver = webdriver.Firefox(options=options)

driver.get("https://symptomchecker.webmd.com/")
sleep(15)

iframes = driver.find_elements_by_tag_name('iframe')

for iframe in iframes:
  try:
    driver.switch_to.frame(iframe)
    WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.CSS_SELECTOR, '.saveAndExit'))).click()
    driver.switch_to.default_content()
    break
  except TimeoutException:
    print('Not found in iframe')
    driver.switch_to.default_content()
  except StaleElementReferenceException:
    print('Stale iframe found... passing')
    driver.switch_to.default_content()

try:
  WebDriverWait(driver, 20).until(EC.alert_is_present())
  driver.switch_to.alert.accept()
  driver.switch_to.default_content()
except TimeoutException:
  print('Alert not found')


driver.find_element_by_id('age').send_keys('22')
driver.find_element_by_id('male').click()
driver.find_element_by_css_selector('button.webmd-button:nth-child(3)').click()
driver.get_cookies

symptoms = ['cough',
'itchy throat',
'laryngeal pain',
'voice is hoarse']

input_field = driver.find_element_by_css_selector('.form-control')
for symptom in symptoms:
  input_field.send_keys(symptom)
  sleep(2)
  input_field.send_keys(Keys.DOWN)
  input_field.send_keys(Keys.ENTER)

driver.find_element_by_css_selector('.solid-button').click()
driver.find_element_by_css_selector('button.solid-button:nth-child(1)').click()
