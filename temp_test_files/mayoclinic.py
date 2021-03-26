from selenium.common.exceptions import TimeoutException
from selenium import webdriver
from selenium.webdriver.firefox.options import Options
from selenium.webdriver.support.ui import WebDriverWait
from requests import get

print('Starting Webdriver...')
options = Options()
options.headless = True
options.page_load_strategy = 'none'
driver = webdriver.Firefox(options=options)

search_argument = input('What do you want to search ? :')

driver.get('https://www.mayoclinic.org/search/search-results?q=' + search_argument)
try:
    first_result = WebDriverWait(driver, timeout=1).until(lambda a: a.find_element_by_css_selector('.noimg:nth-child(1) a'))
    first_result.click()
except TimeoutException:
    print('No results for ' + search_argument)
finally:
    driver.quit()

#page_source = get(driver.current_url).text

print(driver.current_url)
print(driver.page_source)



driver.quit()

