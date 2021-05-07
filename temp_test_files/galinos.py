from bs4 import BeautifulSoup
import requests
import re

response = requests.get("https://www.galinos.gr/web/drugs/main/lists/drugs/V")

id_number = re.search("table_[0-9]{3}", str(response.content))
id_number = "#" + id_number.group()
response_soup = BeautifulSoup(response.content, "lxml")
table = response_soup.select(str(id_number))
print(table)



